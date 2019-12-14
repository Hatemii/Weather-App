Imports System.Net
Imports System.Web.Script.Serialization

Partial Class VB
    Inherits System.Web.UI.Page

    Protected Sub GetWeatherInfo(sender As Object, e As EventArgs)
        Dim appId As String = "<App Id>"
        Dim url As String = String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", txtCity.Text.Trim(), appId)
        Using client As New WebClient()
            Dim json As String = client.DownloadString(url)

            Dim weatherInfo As WeatherInfo = (New JavaScriptSerializer()).Deserialize(Of WeatherInfo)(json)
            lblCity_Country.Text = weatherInfo.city.name + "," + weatherInfo.city.country
            imgCountryFlag.ImageUrl = String.Format("http://openweathermap.org/images/flags/{0}.png", weatherInfo.city.country.ToLower())
            lblDescription.Text = weatherInfo.list(0).weather(0).description
            imgWeatherIcon.ImageUrl = String.Format("http://openweathermap.org/img/w/{0}.png", weatherInfo.list(0).weather(0).icon)
            lblTempMin.Text = String.Format("{0}°С", Math.Round(weatherInfo.list(0).temp.min, 1))
            lblTempMax.Text = String.Format("{0}°С", Math.Round(weatherInfo.list(0).temp.max, 1))
            lblTempDay.Text = String.Format("{0}°С", Math.Round(weatherInfo.list(0).temp.day, 1))
            lblTempNight.Text = String.Format("{0}°С", Math.Round(weatherInfo.list(0).temp.night, 1))
            lblHumidity.Text = weatherInfo.list(0).humidity.ToString()
            tblWeather.Visible = True
        End Using
    End Sub

    Public Class WeatherInfo
        Public Property city As City
        Public Property list As List(Of List)
    End Class

    Public Class City
        Public Property name As String
        Public Property country As String
    End Class

    Public Class Temp
        Public Property day As Double
        Public Property min As Double
        Public Property max As Double
        Public Property night As Double
    End Class

    Public Class Weather
        Public Property description As String
        Public Property icon As String
    End Class

    Public Class List
        Public Property temp As Temp
        Public Property humidity As Integer
        Public Property weather As List(Of Weather)
    End Class
End Class
