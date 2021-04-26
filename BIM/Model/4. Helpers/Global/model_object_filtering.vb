Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.XtraEditors.Filtering
Imports DevExpress.XtraEditors.Repository

Public Class model_object_filtering
    Private data_search As object_data_search = object_data_search.Instance
    Property criteria_search As String

#Region "constructor"
    Public Sub New(filtering As procedure_filtering)
        InitializeComponent()
        PerformAutoScale()

        'Define el nombre seleccionado para cargar los filtros
        fields_condition(filtering)
    End Sub

    Private Sub module_load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
#End Region

#Region "methods"
    Private Sub fields_condition(filtering As procedure_filtering)
        Try
            CallByName(Me, filtering.ToString, CallType.Method)
        Catch ex As Exception
        End Try
    End Sub

    Private Function criteria_condition() As String
        Dim type_group As GroupOperator = TryCast(object_filtering_criteria.FilterCriteria, GroupOperator)

        If type_group IsNot Nothing Then Return type_conditions(type_group) Else Return type_conditions(object_filtering_criteria.FilterCriteria)
    End Function

    Private Function type_conditions(group_condition As GroupOperator) As String
        Dim condition_string As String = Nothing
        Dim condition_criteria As Object = Nothing

        If group_condition IsNot Nothing Then
            For Each condition As CriteriaOperator In group_condition.Operands
                If TypeOf condition Is BinaryOperator Then condition_criteria = TryCast(condition, BinaryOperator)
                If TypeOf condition Is InOperator Then condition_criteria = TryCast(condition, InOperator)
                If TypeOf condition Is BetweenOperator Then condition_criteria = TryCast(condition, BetweenOperator)
                If TypeOf condition Is UnaryOperator Then condition_criteria = TryCast(condition, UnaryOperator)

                If condition_string Is Nothing Then condition_string = operand_condition(condition_criteria) Else condition_string += condition_group(group_condition.OperatorType) & operand_condition(condition_criteria)
            Next
        End If

        Return condition_string
    End Function

    Private Function type_conditions(condition As CriteriaOperator) As String
        Dim condition_criteria As Object = Nothing

        If TypeOf condition Is BinaryOperator Then condition_criteria = TryCast(condition, BinaryOperator)
        If TypeOf condition Is InOperator Then condition_criteria = TryCast(condition, InOperator)
        If TypeOf condition Is BetweenOperator Then condition_criteria = TryCast(condition, BetweenOperator)
        If TypeOf condition Is UnaryOperator Then condition_criteria = TryCast(condition, UnaryOperator)

        Return operand_condition(condition_criteria)
    End Function

    Public Function operand_condition(operand As BinaryOperator) As String
        Dim value_property As OperandProperty
        Dim value_operand1 As OperandValue

        value_property = TryCast(operand.LeftOperand, OperandProperty)
        value_operand1 = TryCast(operand.RightOperand, OperandValue)

        Return CStr(value_property.PropertyName & operator_type(operand.OperatorType) & Replace(value_type(value_operand1), " ", ""))
    End Function

    Public Function operand_condition(operand As InOperator) As String
        Dim value_property As OperandProperty
        Dim value_operand1 As OperandValue

        value_property = TryCast(operand.LeftOperand, OperandProperty)
        value_operand1 = TryCast(operand.Operands(0), OperandValue)

        Return CStr(value_property.PropertyName & " in (" & Replace(value_operand1.Value, " ", "") & ")")
    End Function

    Public Function operand_condition(operand As UnaryOperator) As String
        Dim operand_object As InOperator = TryCast(operand.Operand, InOperator)
        Dim value_property As OperandProperty
        Dim value_operand1 As OperandValue

        value_property = TryCast(operand_object.LeftOperand, OperandProperty)
        value_operand1 = TryCast(operand_object.Operands(0), OperandValue)

        Return CStr(value_property.PropertyName & " not in (" & Replace(value_operand1.Value, " ", "") & ")")
    End Function

    Public Function operand_condition(operand As BetweenOperator) As String
        Dim value_property As OperandProperty
        Dim value_operand1 As OperandValue
        Dim value_operand2 As OperandValue

        value_property = TryCast(operand.TestExpression, OperandProperty)
        value_operand1 = TryCast(operand.BeginExpression, OperandValue)
        value_operand2 = TryCast(operand.EndExpression, OperandValue)

        Return CStr(value_property.PropertyName & " between " & value_type(value_operand1) & " and " & value_type(value_operand2))
    End Function

    Private Function operator_type(typeoperator As BinaryOperatorType) As String
        Dim operator_string As String = ""

        If typeoperator = BinaryOperatorType.Equal Then operator_string = "="
        If typeoperator = BinaryOperatorType.Less Then operator_string = "<"
        If typeoperator = BinaryOperatorType.LessOrEqual Then operator_string = "<="
        If typeoperator = BinaryOperatorType.Greater Then operator_string = ">"
        If typeoperator = BinaryOperatorType.GreaterOrEqual Then operator_string = ">="
        If typeoperator = BinaryOperatorType.NotEqual Then operator_string = " <> "

        Return operator_string
    End Function

    Private Function value_type(value_operator As OperandValue) As String
        Dim value_string As String = ""

        If TypeOf value_operator.Value Is Date Then value_string = "'" & CDate(value_operator.Value).ToString("yyyy-MM-dd") & "'"
        If TypeOf value_operator.Value Is Integer Then value_string = CInt(value_operator.Value)
        If TypeOf value_operator.Value Is String Then value_string = "'" & CStr(value_operator.Value).ToString & "'"
        If TypeOf value_operator.Value Is Decimal Then value_string = CDec(value_operator.Value)
        If TypeOf value_operator.Value Is Double Then value_string = CDbl(value_operator.Value)

        Return value_string
    End Function

    Private Function condition_group(typeofoperator As GroupOperatorType) As String
        Dim condition_string As String = ""

        If typeofoperator = GroupOperatorType.And Then condition_string = " and "
        If typeofoperator = GroupOperatorType.Or Then condition_string = " or "

        Return condition_string
    End Function

    Private Function boolean_values(Optional yesno As Boolean = False) As DataTable
        Dim source_table As New DataTable

        With source_table
            .Columns.Add("Id")
            .Columns.Add("display_name")

            .Rows.Add(1, If(yesno = True, "Si", "Activo"))
            .Rows.Add(0, If(yesno = True, "No", "Inactivo"))
        End With

        Return source_table
    End Function
#End Region

#Region "helpers"
    Sub objects_customer()
        With data_search
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Fecha de creación", "c.creation_date", GetType(Date), New RepositoryDate, FilterColumnClauseClass.DateTime))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Fecha de nacimiento", "c.birth_date", GetType(Date), New RepositoryDate, FilterColumnClauseClass.DateTime))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Edad", "datediff(year,c.birth_date,getdate())", GetType(Integer), New RepositoryItemTextEdit, FilterColumnClauseClass.String))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Agencia", "c.agency_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spEntitiesBussinesAgency_Search, "a.row_visible=1"), "agency_name"), FilterColumnClauseClass.Lookup))
            ''object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Ciudad", "c.city_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.spSettingsGeneralSearch_Cities, "c.row_visible=1"), "city_name"), FilterColumnClauseClass.Lookup))
            '' object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Departamento", "i.depto_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.spSettingsGeneralSearch_Deparment, "d.row_visible=1"), "department_name"), FilterColumnClauseClass.Lookup))
            ''object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Tipo de documento", "c.document_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.spSettingsGeneralSearch_Document, "d.row_visible=1"), "alias_document"), FilterColumnClauseClass.Lookup))
            ''object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Genero", "c.genre_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.spSettingsGeneralSearch_Genre, "g.row_visible=1"), "genre_name"), FilterColumnClauseClass.Lookup))

            ''Campos especiales de busqueda
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Total Ventas", "dbo.fn_relationship_customer_wholesales(c.Id)", GetType(Integer), New RepositoryItemTextEdit, FilterColumnClauseClass.String))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Ultima Compra", "dbo.fn_relationship_customer_lastpurchase(c.Id)", GetType(Date), New RepositoryDate, FilterColumnClauseClass.DateTime))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Puntaje", "convert(smallint,c.credit_score)", GetType(Integer), New RepositoryItemTextEdit, FilterColumnClauseClass.String))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Cupo", "convert(decimal(18,2),c.credit_limit)", GetType(Double), New RepositoryItemTextEdit, FilterColumnClauseClass.String))
            ''object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("CEE", "c.knowledge_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.relationship_customer_knowledge_search, "row_visible=1"), "knowledge_name"), FilterColumnClauseClass.Lookup))
            ''object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Fuente", "c.source_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.relationship_customer_person_sources, "row_visible=1"), "source_name"), FilterColumnClauseClass.Lookup))
            ''object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Nombre de consultor", "c.person_code", GetType(Integer), New RepositoryChecked(.fn_procedure_search(dop.entities_workforce_persons_listed, "p.row_visible=1"), "Nombre del funcionario"), FilterColumnClauseClass.Lookup))
        End With
    End Sub

    Sub objects_personal()
        With data_search
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Fecha de creación", "p.creation_date", GetType(Date), New RepositoryDate, FilterColumnClauseClass.DateTime))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Fecha de nacimiento", "p.birth_date", GetType(Date), New RepositoryDate, FilterColumnClauseClass.DateTime))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Edad", "datediff(year,p.birth_date,getdate())", GetType(Integer), New RepositoryItemTextEdit, FilterColumnClauseClass.String))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Agencia", "p.agency_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spEntitiesBussinesAgency_Search, "a.row_visible=1"), "agency_name"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Ciudad", "p.city_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spSettingsGeneralSearch_Municipality, "m.row_visible=1"), "municipality_name"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Departamento", "c.depto_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spSettingsGeneralSearch_Department, "d.row_visible=1"), "department_name"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Tipo de documento", "p.document_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spSettingsGeneralSearch_Catalogue, "c.row_visible=1 and c.catalogue_type='D'"), "acronym"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Genero", "p.genre_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spSettingsGeneralSearch_Catalogue, "c.row_visible=1 and c.catalogue_type='G'"), "display_name"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Cargo", "p.position_code", GetType(Integer), New RepositoryChecked(.procedure_search(dop.spEntitiesBusinessPosition_Search, "p.row_visible=1"), "position_name"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Habeas Data", "p.opt_in", GetType(Integer), New RepositoryListed(boolean_values(True), "display_name"), FilterColumnClauseClass.Lookup))
            'object_filtering_criteria.FilterColumns.Add(New UnboundFilterColumn("Estado", "p.row_visible", GetType(Integer), New RepositoryListed(boolean_values(), "display_name"), FilterColumnClauseClass.Lookup))
        End With
    End Sub
#End Region

#Region "options"
    Private Sub saved_option(sender As Object, e As EventArgs) Handles object_button_saved.Click
        'Obtiene la condicion de busqueda seleccionada según los filtros usados
        criteria_search = criteria_condition()

        If criteria_search <> "" Then
            TryCast(Me.Parent, model_object_flyout).Close()
        End If
    End Sub
#End Region
End Class
Public Enum procedure_filtering
    objects_customer
    objects_personal
End Enum
