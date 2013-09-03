Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	NameForeignConstraints
	
	MsgBox("Done")
End Sub

Sub NameForeignConstraints
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim MyRelation As Relationship
	Dim Count As Integer
	Dim Indx As Integer
	Dim ParentTableName As String
	Dim ChildTableName As String
	Dim RelationshipName As String
	Dim RelationshipNameBase As String
	Dim DuplicateRelationshipIndx As Integer
	Dim RelationshipNameUsed As Boolean
	Dim SetBusinessName As Boolean

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	Count = MyModel.Relationships.Count
	ReDim RelationshipNames(1 To Count) As String
	
	ReDim FixupNames(0) As String
	Dim FixupNamesIndx As Integer
	
	ReDim FixupBusinessNames(0) As String
	Dim FixupBusinessNamesIndx As Integer

	FixupNamesIndx = 0
	FixupBusinessNamesIndx = 0
	Indx = 1

	For Each MyRelation In MyModel.Relationships

		SetBusinessName = True

		If Left(MyRelation.BusinessName, 4) = "FKX_" Then
			RelationshipNameBase = "FK_" & Mid(MyRelation.BusinessName, 5, Len(MyRelation.BusinessName))
			SetBusinessName = False
		Else
			ParentTableName = MyRelation.ParentEntity.TableName
			ChildTableName = MyRelation.ChildEntity.TableName
	
			'Remove spaces from table names
			ParentTableName = Replace(ParentTableName," ","")
			ChildTableName = Replace(ChildTableName," ","")
	
			RelationshipNameBase = "FK_" & ParentTableName & "_" & ChildTableName
		End If

		RelationshipName = RelationshipNameBase
		DuplicateRelationshipIndx = 1
		RelationshipNameUsed = True

		Do While RelationshipNameUsed
			RelationshipNameUsed = False
			For i = 1 To Indx
				If RelationshipNames(i) = RelationshipName Then
					RelationshipNameUsed = True
				End If
			Next i

			If RelationshipNameUsed Then
				DuplicateRelationshipIndx = DuplicateRelationshipIndx + 1
				RelationshipName = RelationshipNameBase & "_" & DuplicateRelationshipIndx
			End If
		Loop

		If DuplicateRelationshipIndx > 1 Then
			ReDim Preserve FixupNames(FixupNamesIndx + 1)
			FixupNames(FixupNamesIndx + 1) = RelationshipNameBase
			FixupNamesIndx = FixupNamesIndx + 1
			
			If SetBusinessName Then
				ReDim Preserve FixupBusinessNames(FixupBusinessNamesIndx + 1)
				FixupBusinessNames(FixupBusinessNamesIndx + 1) = RelationshipNameBase
				FixupBusinessNamesIndx = FixupBusinessNamesIndx + 1
			End If
		End If

		If SetBusinessName Then
			MyRelation.BusinessName = RelationshipName
		End If
		
		MyRelation.Name = RelationshipName
		RelationshipNames(Indx) = RelationshipName
		Indx = Indx + 1
	Next

	For Each MyRelation In MyModel.Relationships
		For i = 1 To FixupNamesIndx
			If FixupNames(i) = MyRelation.Name Then
				MyRelation.Name = MyRelation.Name & "_1"
			End If
		Next i

		For i = 1 To FixupBusinessNamesIndx
			If FixupBusinessNames(i) = MyRelation.BusinessName Then
				MyRelation.BusinessName = MyRelation.BusinessName & "_1"
			End If
		Next i
	Next
End Sub
