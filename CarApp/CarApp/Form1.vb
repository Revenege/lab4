''' <summary>
''' Programmer: Scott Jenkins
''' Project Purpose: This program is car auditiing program. It takes user input for make, model, price, year, and if its used and 
''' add its to a viewlist. The values in that list can be edited.
''' Date of Completion: March 13th 2019
''' </summary>

Public Class frmCarInvent

    'Form level Variables
    Private carList As New SortedList           'Sorted list of all car objects
    Private currentID As String = String.Empty  'The ID of what the user has selected
    Private edit As Boolean = False             'Determines when the form is allowed to be edited
    Private Const LOWER_BOUND As Integer = 1980 'Lower bound for year dropdown. Default of 1980, arbitrary
    Private Const UPPER_BOUND As Integer = 2019 'Upper bound for year dropdwon. Default of 2019, one year after the current year per industry standard
    ''' <summary>
    ''' Enter Button - Handles the enter button being clicked. WHen clicked, Validates that the value entered is valid.
    ''' On valid input the form checks to see if the user wants to edit an existing record. If they don't, it adds the new
    ''' record to the list. If they do, it pulls the requests record, allows it to be edited, and then readds it at the same index
    ''' Once a valid input is given and added, the program wipes all input fields for a new input
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        Dim objCar As Car
        Dim newRow As ListViewItem

        'If input is valid, proceed with adding to to the list
        If Validation() Then
            edit = True
            'If new record, add to bottom of list
            If currentID.Trim.Length = 0 Then
                objCar = New Car(cmbxMake.Text, txtModel.Text, CInt(cmbxYear.Text), CDbl(txtPrice.Text), chbxNew.Checked)

                carList.Add(objCar.GetIdNumber.ToString, objCar)
            Else 'Otherwise, edit the existing record
                objCar = CType(carList.Item(currentID), Car)

                objCar.QualityStatus = chbxNew.Checked
                objCar.ModelStatus = txtModel.Text
                objCar.MakeStatus = cmbxMake.Text
                objCar.PriceStatus = CDbl(txtPrice.Text)
                objCar.YearStatus = CInt(cmbxYear.Text)
            End If

            'Empty the  listView for readdition
            listOutput.Items.Clear()

            'Add the values to the correct columns of the listView
            For Each carEntry As DictionaryEntry In carList
                newRow = New ListViewItem()
                objCar = CType(carEntry.Value, Car)

                newRow.Checked = objCar.QualityStatus()
                newRow.SubItems.Add(objCar.GetIdNumber.ToString())
                newRow.SubItems.Add(objCar.MakeStatus)
                newRow.SubItems.Add(objCar.ModelStatus)
                newRow.SubItems.Add(objCar.YearStatus.ToString())
                newRow.SubItems.Add(objCar.PriceStatus.ToString("$0.00"))

                listOutput.Items.Add(newRow)

            Next carEntry
            edit = False 'Disable edit mode
        End If
        Reset() 'Clear the listView
    End Sub

    ''' <summary>
    ''' Validation Function - Preforms input validation to ensure no blank input, and no impossible input
    ''' </summary>
    ''' <returns>Boolean</returns>
    Private Function Validation() As Boolean
        'local variables
        Dim valid As Boolean = True 'flag for valid input. Defaults to true
        Dim errorMessage As String = String.Empty 'Error Meesage that appears in the message text box

        'Validation on the Make. If no choice, Error
        If cmbxMake.SelectedIndex = -1 Then
            errorMessage += "Please select the make of vehicle" & vbCrLf
            valid = False
        End If

        'Validation on the year. If no choice, Error
        If cmbxYear.SelectedIndex = -1 Then
            errorMessage += "Please select a year of the vehicle" & vbCrLf
            valid = False
        End If

        'Validation on the Model. If no choice, Error
        If txtModel.Text.Trim.Length = 0 Then
            errorMessage += "Please select a model of the vehicle" & vbCrLf
            valid = False
        End If

        'Validation on price. If no Choice, non-numeric, or less then zero, Error
        If txtPrice.Text.Trim.Length = 0 Then
            errorMessage += "Please select how much the vehicle costs" & vbCrLf
            valid = False
        ElseIf IsNumeric(txtPrice.Text.Trim) = False Then
            errorMessage += "Please enter the price as a number, not a word"
            valid = False
        ElseIf IsNumeric(txtPrice.Text.Trim) = True Then
            If CDbl(txtPrice.Text.Trim) < 0 Then
                errorMessage += "Price can not be less then 0 dollars"
            End If
        End If

        'If input is not valid, output the error messages
        If valid = False Then
            txtMessages.Text = "You have the following errors" & vbCrLf & errorMessage
        End If

        Return valid
    End Function

    ''' <summary>
    ''' Reset Subroutine - Sets all form level variables, and resets texts and combo boxes to default
    ''' </summary>
    Private Sub Reset()
        currentID = String.Empty
        chbxNew.Checked = False
        cmbxMake.SelectedIndex = -1
        cmbxYear.SelectedIndex = -1
        txtModel.Text = String.Empty
        txtPrice.Text = String.Empty

        currentID = String.Empty
    End Sub

    ''' <summary>
    '''ItemCheck Subroutine - If the user tries to alter the checkboxes, stops them
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub listOuput_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles listOutput.ItemCheck
        If edit = False Then
            e.NewValue = e.CurrentValue
        End If
    End Sub

    ''' <summary>
    ''' SelectedIndexChanged - If the use selects something in the listview, allows the editing of that entry.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub listOutput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listOutput.SelectedIndexChanged
        Const ID_INDEX As Integer = 1

        currentID = listOutput.Items(listOutput.FocusedItem.Index).SubItems(ID_INDEX).Text

        Dim editCarObj As Car = CType(carList.Item(currentID), Car)

        chbxNew.Checked = editCarObj.QualityStatus()
        cmbxMake.Text = editCarObj.MakeStatus()
        cmbxYear.Text = editCarObj.YearStatus().ToString
        txtModel.Text = editCarObj.ModelStatus().ToString
        txtPrice.Text = editCarObj.PriceStatus().ToString

    End Sub

    ''' <summary>
    ''' Reset button - Calls the Reset() subroutine on clicking the button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    ''' <summary>
    ''' Exit Button - Ends the program
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Form Load - On form load, fills the year textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmCarInvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Populates the year drop down without me needing to add hundreds of years

        For year As Integer = LOWER_BOUND To UPPER_BOUND
            cmbxYear.Items.Add(year.ToString)
        Next
    End Sub
End Class
