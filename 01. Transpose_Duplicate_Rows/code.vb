Sub TransposeRowsToColumn()
'Author:Sreejith Muralidharan
'Transponse Row to Column will allow you to move duplicate values to the column.

Dim xArr1 As Variant
Dim xArr2 As Variant
Dim InputRng As Range, OutRng As Range
Dim xRows As Long
xTitleId = "Convert Duplicate Rows to Column"
Set InputRng = Application.Selection
Set InputRng = Application.InputBox("Range :", xTitleId, InputRng.Address, Type:=8)
Set OutRng = Application.InputBox("Out put to (single cell):", xTitleId, Type:=8)
Set OutRng = OutRng.Range("A1")
xArr1 = InputRng.Value
t = UBound(xArr1, 2): xRows = 1
With CreateObject("Scripting.Dictionary")
    .CompareMode = 1
    For i = 2 To UBound(xArr1, 1)
        If Not .exists(xArr1(i, 1)) Then
            xRows = xRows + 1: .Item(xArr1(i, 1)) = VBA.Array(xRows, t)
            For ii = 1 To t
                xArr1(xRows, ii) = xArr1(i, ii)
            Next
        Else
            xArr2 = .Item(xArr1(i, 1))
            If UBound(xArr1, 2) < xArr2(1) + t - 1 Then
                ReDim Preserve xArr1(1 To UBound(xArr1, 1), 1 To xArr2(1) + t - 1)
                For ii = 2 To t
                    xArr1(1, xArr2(1) + ii - 1) = xArr1(1, ii)
                Next
            End If
            For ii = 2 To t
                xArr1(xArr2(0), xArr2(1) + ii - 1) = xArr1(i, ii)
            Next
            xArr2(1) = xArr2(1) + t - 1: .Item(xArr1(i, 1)) = xArr2
        End If
    Next
End With
OutRng.Resize(xRows, UBound(xArr1, 2)).Value = xArr1
End Sub
