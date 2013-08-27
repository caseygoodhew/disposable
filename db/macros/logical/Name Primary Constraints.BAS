Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	NamePrimaryConstraints
	
	MsgBox("Done")
End Sub

Sub NamePrimaryConstraints
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim MyIndex As Index

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		For Each MyIndex In MyEntity.Indexes

			' make sure index/constraint is a primary key
			If MyIndex.IsPK = True Then
				MyIndex.Name = "PK_" + MyEntity.TableName
			End If

		Next MyIndex
	Next MyEntity
End Sub
