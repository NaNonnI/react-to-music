Imports System.IO
Imports System.Security.Cryptography
Imports Un4seen.Bass
Public Class init

    'Register for free here http://bass.radio42.com/bass_register.html

    Dim ubassEmail = ""
    Dim ubassApi = ""

    Dim RandomPos As New Random
    Dim rnd As New Random
    Dim rndshow As New Random
    Dim RetVal As Integer
    Dim maxBox = 35
    Dim curBox = 0
    Dim gravity = 0
    Dim gravityForce = 10
    Dim sensiMusic = 130
    Dim sensiBass = 113
    Dim onlyBass = False
    Dim rightch
    Dim leftch
    Dim temp
    Dim temp0
    Dim temp2
    Dim temp3

    Dim spam1 = New spam
    Dim spam2 = New spam
    Dim spam3 = New spam
    Dim spam4 = New spam
    Dim spam5 = New spam

    Dim spam1max = rndshow.Next(1, 5)
    Dim spam2max = rndshow.Next(1, 5)
    Dim spam3max = rndshow.Next(1, 5)
    Dim spam4max = rndshow.Next(1, 5)
    Dim spam5max = rndshow.Next(1, 5)

    Dim MusicStream = My.Settings.MusicStream

    Private Sub BassInt_Tick(sender As Object, e As EventArgs) Handles BassInt.Tick
        RetVal = Bass.BASS_ChannelGetLevel(MusicStream)
        rightch = Un4seen.Bass.Utils.HighWord(RetVal) / 270
        leftch = Un4seen.Bass.Utils.LowWord(RetVal) / 270
        If rightch <= sensiMusic Then
            temp2 = rightch
        Else
            temp2 = sensiMusic
        End If
        If leftch <= sensiMusic Then
            temp0 = leftch
        Else
            temp0 = sensiMusic
        End If
        Label3.Text = leftch.ToString()
        Me.leftlevel()
        Me.rightlevel()

        If gravity < gravityForce Then
            gravity = gravityForce
        Else
            If temp >= sensiBass Then
                gravity = gravity + gravityForce * 2
                Musicbass()
            Else
                gravity = gravity - gravityForce * 2
            End If
        End If

        If temp0 = -0 Then
            'Me.Close()
        End If

        Label4.Text = gravity / 100.ToString()
        Dim result = Math.Max(Math.Min(gravity, 100), 0)
        ProgressBar1.Value = result
        Musicspam()
    End Sub

    Sub MusicOpenSpam()
        spam1.Show()
        spam1.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))

        spam2.Show()
        spam2.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))

        spam3.Show()
        spam3.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))

        spam4.Show()
        spam4.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))

        spam5.Show()
        spam5.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))

        If onlyBass Then
            HideSpam()
        Else
            ShowSpam()
        End If
    End Sub

    Sub Musicbass()
        If onlyBass Then ShowSpam()

        spam1.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))
        spam1.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))

        spam2.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))
        spam2.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))

        spam3.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))
        spam3.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))

        spam4.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))
        spam4.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))

        spam5.DesktopLocation = New Point(RandomPos.Next(1, 1920), RandomPos.Next(1, 1080))
        spam5.BackColor = Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255))

        If onlyBass Then HideSpam()
    End Sub
    Sub Musicspam()
        spam1.Height = temp * spam1max
        spam1.Width = temp3 * spam1max

        spam2.Height = temp * spam2max
        spam2.Width = temp3 * spam2max

        spam3.Height = temp * spam3max
        spam3.Width = temp3 * spam3max

        spam4.Height = temp * spam4max
        spam4.Width = temp3 * spam4max

        spam5.Height = temp * spam5max
        spam5.Width = temp3 * spam5max
    End Sub

    Sub HideSpam()
        spam1.Opacity = 0
        spam2.Opacity = 0
        spam3.Opacity = 0
        spam4.Opacity = 0
        spam5.Opacity = 0
    End Sub

    Sub ShowSpam()
        spam1.Opacity = 255
        spam2.Opacity = 255
        spam3.Opacity = 255
        spam4.Opacity = 255
        spam5.Opacity = 255
    End Sub

    Sub leftlevel()
        If temp = temp0 Then
            Return
        ElseIf temp0 > temp Then
            temp = temp0
        ElseIf temp0 < temp Then
            temp -= 2
        Else : Return
        End If
        Label2.Text = temp.ToString()
    End Sub
    Sub rightlevel()
        If temp3 = temp2 Then
            Return
        ElseIf temp2 > temp3 Then
            temp3 = temp2
        ElseIf temp2 < temp3 Then
            temp3 -= 2
        Else : Return
        End If
        Label1.Text = temp3.ToString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "Audio Files|*.mp3"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            'Bass.BASS_ChannelPlay(Stream, False)
            Label5.Text = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)

            Un4seen.Bass.BassNet.Registration(ubassEmail, ubassApi)
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero)
            ProgressBar1.Maximum = 100
            Me.TopMost = My.Settings.top_most
            MusicOpenSpam()

            Bass.BASS_ChannelStop(MusicStream)
            Bass.BASS_StreamFree(MusicStream)
            MusicStream = Bass.BASS_StreamCreateFile(OpenFileDialog1.FileName, 0, 0, BASSFlag.BASS_DEFAULT)
            Bass.BASS_ChannelPlay(MusicStream, False)
            BassInt.Enabled = True
        End If
    End Sub

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles TextBox.TextChanged
        sensiMusic = TextBox.Text
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        sensiBass = TextBox1.Text
    End Sub

    Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox.CheckedChanged
        If CheckBox.Checked Then
            onlyBass = True
            HideSpam()
        Else
            onlyBass = False
            ShowSpam()
        End If
    End Sub
End Class