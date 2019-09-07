Module CoinspotAPITest

    Private key As String = ""
    Private secret As String = ""

    Sub Main()
        If Not APIKeysSupplied() Then
            Console.Clear()
            Console.WriteLine("API Keys not provided.  These need to be hardcoded before building the test app")
            Console.ReadKey()
            Exit Sub
        End If
        MainMenu()
    End Sub

    Function APIKeysSupplied() As Boolean
        If key = "" OrElse secret = "" Then Return False
        Return True
    End Function

    Sub MainMenu()
        Console.Clear()
        Console.WriteLine("Select an option (not case sensitive)")
        Console.WriteLine("A. Get All Balances")
        Console.WriteLine("B. Get Coin Balance (CoinType - Required) Fails with 404")
        Console.WriteLine("C. Get Deposits")
        Console.WriteLine("D. Get Withdrawals")
        Console.WriteLine("E. Get All Transactions")
        Console.WriteLine("F. Get Coin Transactions (CoinType - Required)")
        Console.WriteLine("G. Get Open Transactions")
        Console.WriteLine("H. Get Coin Open Transactions (CoinType - Required)")
        Console.WriteLine("I. Get Send/Receive Transactions")
        Console.WriteLine("J. Get Affiliate Payments")
        Console.WriteLine("K. Get Referral Payments")
        Select Case LCase(Console.ReadKey().Key)
            Case ConsoleKey.A
                GetAllBalances()
            Case ConsoleKey.B
                GetCoinBalances()
            Case ConsoleKey.C
                GetDeposits()
            Case ConsoleKey.D
                GetWithdrawals()
            Case ConsoleKey.E
                GetAllTransactions()
            Case ConsoleKey.F
                GetCoinTransactions()
            Case ConsoleKey.G
                GetOpenTransactions()
            Case ConsoleKey.H
                GetCoinOpenTransactions()
            Case ConsoleKey.I
                GetSendReceiveTransactions()
            Case ConsoleKey.J
                GetAffiliatePayments()
            Case ConsoleKey.K
                GetReferralPayments()
        End Select
        MainMenu()
    End Sub

    Sub RequestCS(EndPointURL As String, JSONParameters As String)
        Dim cs = New CoinspotAPI.CoinspotAPIHandler(key, secret)
        Dim csresponse = cs.CallAPI(EndPointURL, JSONParameters)
        Console.Write(csresponse)
        Console.WriteLine("")
        System.Windows.Forms.Clipboard.SetText(csresponse)
        Console.WriteLine("Press any key")
        Console.ReadKey()
    End Sub

    Sub GetAllBalances()
        Console.Clear()
        RequestCS("/api/ro/my/balances", "{}")
    End Sub

    Sub GetCoinBalances()
        Console.Clear()
        Console.WriteLine("Enter coin code")
        Dim CoinType = Console.ReadLine()
        RequestCS("/api/ro/my/balances/" & CoinType, "{""cointype"":""" & CoinType & """}")
    End Sub

    Sub GetDeposits()
        Console.Clear()
        RequestCS("/api/ro/my/deposits", "{}")
    End Sub

    Sub GetWithdrawals()
        Console.Clear()
        RequestCS("/api/ro/my/withdrawals", "{}")
    End Sub

    Sub GetAllTransactions()
        Console.Clear()
        RequestCS("/api/ro/my/transactions", "{}")
    End Sub

    Sub GetCoinTransactions()
        Console.Clear()
        Console.WriteLine("Enter coin code")
        Dim CoinType = Console.ReadLine()
        RequestCS("/api/ro/my/transactions/" & CoinType, "{""cointype"":""" & CoinType & """}")
    End Sub

    Sub GetOpenTransactions()
        Console.Clear()
        RequestCS("/api/ro/my/transactions/open", "{}")
    End Sub

    Sub GetCoinOpenTransactions()
        Console.Clear()
        Console.WriteLine("Enter coin code")
        Dim CoinType = Console.ReadLine()
        RequestCS("/api/ro/my/transactions/" & CoinType & "/open", "{""cointype"":""" & CoinType & """}")
    End Sub

    Sub GetSendReceiveTransactions()
        Console.Clear()
        RequestCS("/api/ro/my/sendreceive", "{}")
    End Sub

    Sub GetAffiliatePayments()
        Console.Clear()
        RequestCS("/api/ro/my/affiliatepayments", "{}")
    End Sub

    Sub GetReferralPayments()
        Console.Clear()
        RequestCS("/api/ro/my/referralpayments", "{}")
    End Sub

End Module
