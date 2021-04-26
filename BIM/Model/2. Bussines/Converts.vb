Imports System.IO

Public Module Converts
    Public Function transform_bytes(imagen As Image) As Byte()
        Dim arreglo As Byte() = Nothing
        Try
            If Not imagen Is Nothing Then
                Dim Bin As New MemoryStream
                imagen.Save(Bin, Imaging.ImageFormat.Jpeg)
                arreglo = Bin.ToArray()
            Else
                Return Nothing
            End If
        Catch ex As Exception
        End Try
        Return arreglo
    End Function

    Public Function transform_bytes(document_object As String) As Byte()
        Try
            If document_object IsNot Nothing Then
                Dim file_selected As New FileStream(document_object, FileMode.Open, FileAccess.Read)
                Dim file_bytes() As Byte
                ReDim file_bytes(file_selected.Length)
                file_selected.Read(file_bytes, 0, file_selected.Length)

                Return file_bytes
            End If
        Catch ex As Exception
        End Try

        Return Nothing
    End Function

    Public Function transform_image(imagen As Byte()) As Image
        Try
            If Not imagen Is Nothing Then
                Dim Bin As New MemoryStream(imagen)
                Dim Resultado As Image = Image.FromStream(Bin)
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function json_serialize(value_serialize As Object) As String
        Return Newtonsoft.Json.JsonConvert.SerializeObject(value_serialize)
    End Function

    Public Function json_deserialize(value_deserialize As String) As DataTable
        Try
            Return TryCast(Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(value_deserialize), DataTable)
        Catch ex As Exception
        Finally
        End Try

        Return Nothing
    End Function
End Module
