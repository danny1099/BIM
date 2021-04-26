Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports Excel = Microsoft.Office.Interop.Excel

Public Module External
    <DllImport("kernel32")> Private Function GetPrivateProfileString(lpAppName As String, lpKeyName As String, lpDefault As String, lpReturnedString As System.Text.StringBuilder, nSize As Integer, lpFileName As String) As Integer
    End Function

    Public Function text_local(name As String, item As String, path As String) As String
        Dim sb As New System.Text.StringBuilder(5000)
        Dim profile_code As Integer
        Dim result_text As String

        profile_code = GetPrivateProfileString(name, item, String.Empty, sb, sb.MaxCapacity, path)
        result_text = sb.ToString

        Return result_text
    End Function

    Public Function split_words(split_text As String) As String()
        Return Regex.Split(split_text, "\W+")
    End Function

    Public Sub objects_released(ByVal o As Object)
        Try
            While (Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
End Module
