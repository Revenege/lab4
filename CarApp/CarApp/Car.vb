Option Strict On

Public Class Car

    'Variable Declarations
    Private Shared countNum As Integer          'Stores the total number of  entries. Shared
    Private identificationNum As Integer = 0    'Store the id number of the car
    Private make As String = String.Empty       'Stores the make of the car
    Private model As String = String.Empty      'Stores the model of the car
    Private price As Double = 0.0               'Stores the price of the car
    Private year As Integer = 0                 'Stores the year of that model car
    Private newStatus As Boolean = False        'Stores whether the car is new or not

    '''<summary>
    '''Default Constructor - Creates a new Car Object
    '''</summary>
    Public Sub New()
        countNum += 1
        identificationNum = countNum
    End Sub

    ''' <summary>
    ''' Parameterized Constructor - Creates a new Car Object
    ''' </summary>
    ''' <param name="newMake"></param>
    ''' <param name="newModel"></param>
    ''' <param name="newYear"></param>
    ''' <param name="newPrice"></param>
    ''' <param name="statusUpdate"></param>
    Public Sub New(newMake As String, newModel As String, newYear As Integer, newPrice As Double, statusUpdate As Boolean)
        Me.New()
        make = newMake
        model = newModel
        year = newYear
        price = newPrice
        newStatus = statusUpdate
    End Sub

    ''' <summary>
    ''' Gets the number of entries
    ''' </summary>
    ''' <returns>Integer</returns>
    Public ReadOnly Property GetCount() As Integer
        Get
            Return countNum
        End Get
    End Property

    ''' <summary>
    ''' Gets the ID number of the vehicle
    ''' </summary>
    ''' <returns>Integer</returns>
    Public ReadOnly Property GetIdNumber() As Integer
        Get
            Return identificationNum
        End Get
    End Property

    ''' <summary>
    ''' Gets or Sets the the make of the vehicle
    ''' </summary>
    ''' <returns>String</returns>
    Public Property MakeStatus() As String
        Get
            Return make
        End Get
        Set(value As String)
            make = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the model of the vehicle
    ''' </summary>
    ''' <returns>String</returns>
    Public Property ModelStatus() As String
        Get
            Return model
        End Get
        Set(value As String)
            model = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the price of the vehicle
    ''' </summary>
    ''' <returns>Double</returns>
    Public Property PriceStatus() As Double
        Get
            Return price
        End Get
        Set(value As Double)
            price = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the year of the car
    ''' </summary>
    ''' <returns>Integer</returns>
    Public Property YearStatus() As Integer
        Get
            Return year
        End Get
        Set(value As Integer)
            year = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or Sets whether the car is new or not
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property QualityStatus() As Boolean
        Get
            Return newStatus
        End Get
        Set(value As Boolean)
            newStatus = value
        End Set
    End Property

    ''' <summary>
    ''' Returns a string representation of the car object
    ''' </summary>
    ''' <returns>String</returns>
    Public Function GetCarData() As String
        Dim output = (make.ToString & " " & model.ToString & " " & year.ToString & " " & price.ToString & " " & newStatus.ToString)
        Return output
    End Function

End Class

