Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	NameColumnCheckConstraints
	
	MsgBox("Done")
End Sub

Sub NameColumnCheckConstraints
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim MyColumn As AttributeObj

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		For Each MyColumn In MyEntity.Attributes

			' make sure index/constraint is a primary key
			If MyColumn.CheckConstraint = "" Then
				MyColumn.CheckConstraintName = ""
			Else
				MyColumn.CheckConstraintName = "CHK_" & MyEntity.EntityName & "_" & MyColumn.RoleName
			End If

		Next
	Next
End Sub
