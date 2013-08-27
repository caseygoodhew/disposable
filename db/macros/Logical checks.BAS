'DO NOT MODIFY THIS FILE DIRECTLY
Sub Main
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	
	Set MyDiagram = DiagramManager.ActiveDiagram
		Set MyModel = MyDiagram.ActiveModel
	
		If MyModel.Name <> "Logical" Then
			MsgBox("This can only be applied when the logical model is active")
			Exit Sub
	End If

	SetTableOwner
	NamePrimaryConstraints
	NameForeignConstraints
	ValidateIdentityColumnPositions
	
	MsgBox("Done")
End Sub

'DO NOT MODIFY THIS FILE DIRECTLY
Sub SetTableOwner
	Dim MyDiagram As Diagram
	Dim MyModel As Model
	Dim MyEntity As Entity
	Dim DefaultOwner As String

	DefaultOwner = "DISPOSABLE"

	Set MyDiagram = DiagramManager.ActiveDiagram
	Set MyModel = MyDiagram.ActiveModel

	For Each MyEntity In MyModel.Entities
		If MyEntity.Owner = "" Then
			MyEntity.Owner = DefaultOwner
		End If
	Next MyEntity
End Sub

'DO NOT MODIFY THIS FILE DIRECTLY
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

'DO NOT MODIFY THIS FILE DIRECTLY
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

'DO NOT MODIFY THIS FILE DIRECTLY
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
