Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	ValidateIdentityColumnPositions
	
	MsgBox("Done")
End Sub

Sub ValidateIdentityColumnPositions
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim MyIndex As Index
	Dim MyColumn As AttributeObj
	Dim MyIndexColumn As IndexColumn
	Dim ColumnId As Integer
	Dim InvalidTablesIndx As Integer
	ReDim InvalidTables(0) As String
	Dim Msg As String

	InvalidTablesIndx = 0

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities

		ColumnId = -1

		For Each MyColumn In MyEntity.Attributes
			If MyColumn.Identity Then
				ColumnId = MyColumn.ID
				Exit For
			End If
		Next

		If ColumnId <> -1 Then
			For Each MyIndex In MyEntity.Indexes
				If MyIndex.IsPK = True Then
					For Each MyIndexColumn In MyIndex.IndexColumns
						If MyIndexColumn.ID = ColumnId Or MyIndexColumn.SequenceNo = 1 Then
							If MyIndexColumn.ID <> ColumnId Or MyIndexColumn.SequenceNo <> 1 Then
								InvalidTablesIndx = InvalidTablesIndx +1
								ReDim Preserve InvalidTables(InvalidTablesIndx)
								InvalidTables(InvalidTablesIndx) = MyEntity.EntityName
								Exit For
							End If
						End If
					Next
				End If
			Next MyIndex
		End If
	Next MyEntity

	If InvalidTablesIndx <> 0 Then
		Msg = "The following table(s) have Identiy columns where were not in the first postion of their primary keys:" & vbCrLf
		For i = 1 To InvalidTablesIndx
			Msg = Msg & vbCrLf  & InvalidTables(i)
		Next i

		Msg = Msg & vbCrLf  & vbCrLf  & "These table(s) will NOT be created successfully in MySQL."

		MsgBox(Msg)
	End If
End Sub







