Imports System.Security.Cryptography
Imports System.Net
Imports System.Text
Imports System.IO

Public Class CoinspotAPIHandler

    Private BaseURL = "https://www.coinspot.com.au"

    Private _APIKey As String = ""
    Private _APISecret As String = ""

    ''' <summary>
    ''' create new object, store keys
    ''' </summary>
    ''' <param name="APIKey"></param>
    ''' <param name="APISecret"></param>
    Public Sub New(APIKey As String, APISecret As String)
        _APIKey = APIKey
        _APISecret = APISecret
    End Sub

    ''' <summary>
    ''' sign request in HMAC SHA512 and call Coinspot API base URL and endpoint with supplied parameters, nonce is generated for you
    ''' </summary>
    ''' <param name="EndPoint">endpoint/path as specified by the Coinspot API docs</param>
    ''' <param name="JSONParameters">a JSON string of parameters as required by the Coinspot API endpoint EG {"cointype":"BTC", "amount":2.4}</param>
    ''' <returns>JSON string returned from endpoint</returns>
    Public Function CallAPI(EndPoint As String, JSONParameters As String) As String
        ' Configure ServicePointManager
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11
        ServicePointManager.Expect100Continue = True

        Dim EndpointURL = BaseURL & EndPoint

        ' Use milliseconds instead of seconds
        Dim Nonce As Long = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds

        ' Format nonce as a STRING (with quotes) to match C# version
        Dim NonceParameter = """nonce""" & ":""" & Nonce.ToString() & """" & IIf(Replace(JSONParameters, " ", "") = "{}", "", ",")

        Dim FinalParameters = Trim(JSONParameters).Insert(1, NonceParameter)
        Dim FinalParameterBytes = Encoding.UTF8.GetBytes(FinalParameters)
        Dim SignedData = SignData(FinalParameterBytes)

        Dim WR As HttpWebRequest = HttpWebRequest.Create(EndpointURL)
        WR.Method = "POST"
        WR.Headers.Add("key", _APIKey)
        WR.Headers.Add("sign", SignedData.ToLower)
        WR.ContentType = "application/json"
        WR.ContentLength = FinalParameterBytes.Length

        Dim ResponseText = ""
        Try
            Using st = WR.GetRequestStream
                st.Write(FinalParameterBytes, 0, FinalParameterBytes.Length)
            End Using
            Dim RS = WR.GetResponse().GetResponseStream()
            Dim Reader = New StreamReader(RS)
            ResponseText = Reader.ReadToEnd
        Catch ex As Exception
            ResponseText = "{""exception""" & ":""" & ex.ToString & """}"
        End Try

        Return ResponseText
    End Function

    ''' <summary>
    ''' compute HMAC
    ''' </summary>
    ''' <param name="JSONData">byte array of full JSON parameter list including nonce</param>
    ''' <returns>HMAC SHA512 encoded string of JSONData</returns>
    Private Function SignData(JSONData As Byte()) As String

        Dim HMAC = New HMACSHA512(Encoding.UTF8.GetBytes(_APISecret))
        Dim EncodedBytes() = HMAC.ComputeHash(JSONData)

        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To EncodedBytes.Length - 1
            stringBuilder.Append(EncodedBytes(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()



        'Dim EncodedString = System.BitConverter.ToString(EncodedBytes)

        'Return EncodedString.Replace("-", "")

    End Function

End Class
