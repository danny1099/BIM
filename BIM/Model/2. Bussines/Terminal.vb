Imports System.Deployment.Application
Imports System.Net.NetworkInformation
Imports System.Net

Public Module Terminal
    Public Function ip_address() As String
        Return Dns.GetHostEntry(My.Computer.Name).AddressList.FirstOrDefault(Function(i) i.AddressFamily = Sockets.AddressFamily.InterNetwork).ToString()
    End Function

    Public Function pc_name() As String
        Return My.Computer.Name.ToString
    End Function

    Public Function pc_physical() As String
        Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces
        Dim mac As String = nics(0).GetPhysicalAddress.ToString
        Return mac
    End Function

    Public Function ethernet_status() As Boolean
        Return My.Computer.Network.IsAvailable
    End Function

    Public Function random_number(Optional digit_lengt As String = "D10") As String
        Dim rng As New Random
        Dim number As Integer = rng.Next(1, 2000000000)
        Dim digits As String = number.ToString(digit_lengt)

        Return digits
    End Function

    Public Function random_text(longitude As Integer) As String
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890"
        Dim res As New System.Text.StringBuilder()
        Dim rnd As New Random()

        While 0 < Math.Max(System.Threading.Interlocked.Decrement(longitude), longitude + 1)
            res.Append(caracteres(rnd.[Next](caracteres.Length)))
        End While

        Return res.ToString()
    End Function

    Public Function text_spaces(text_search As String) As String
        Return text_search.Substring(0, text_search.IndexOf(" "))
    End Function

    Public Function number_version(Optional use_tag As Boolean = True) As String
        Dim version_number_ As String = Nothing

        Try
            If ApplicationDeployment.IsNetworkDeployed Then
                With ApplicationDeployment.CurrentDeployment.CurrentVersion
                    version_number_ = If(use_tag = True, "Versión: ", "") & .Major & "." & .Minor & "." & .Build & "." & .Revision
                End With
            Else
                version_number_ = If(use_tag = True, "Versión: ", "") & My.Application.Info.Version.ToString
            End If

        Catch ex As Exception
        Finally
        End Try

        Return version_number_
    End Function

    Public Function text_extension(file_name As String) As String
        Return New IO.FileInfo(file_name).Extension
    End Function

    Public Function text_filename(file_name As String) As String
        Return New IO.FileInfo(file_name).Name
    End Function

    Public Function system_version() As String
        Return My.Computer.Info.OSFullName.ToString
    End Function
End Module
