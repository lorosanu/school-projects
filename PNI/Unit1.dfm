object Form1: TForm1
  Left = 76
  Top = 172
  Width = 804
  Height = 678
  HorzScrollBar.Visible = False
  VertScrollBar.Visible = False
  AutoSize = True
  BorderStyle = bsSizeToolWin
  Caption = 'Transformari Geometrice'
  Color = clAppWorkSpace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -19
  Font.Name = 'MS Sans Serif'
  Font.Style = [fsBold]
  Menu = MainMenu1
  OldCreateOrder = False
  Visible = True
  PixelsPerInch = 96
  TextHeight = 24
  object Image1: TImage
    Left = 16
    Top = 24
    Width = 530
    Height = 400
    AutoSize = True
    Center = True
    Stretch = True
    OnMouseMove = Image1MouseMove
  end
  object Image2: TImage
    Left = 632
    Top = 457
    Width = 160
    Height = 160
    Transparent = True
  end
  object Splitter1: TSplitter
    Left = 0
    Top = 0
    Width = 3
    Height = 624
    Cursor = crHSplit
  end
  object GroupBox1: TGroupBox
    Left = 579
    Top = 20
    Width = 217
    Height = 321
    Caption = 'Color'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 0
    Visible = False
    object Label5: TLabel
      Left = 24
      Top = 32
      Width = 29
      Height = 16
      Caption = 'Red:'
      Color = clAppWorkSpace
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentColor = False
      ParentFont = False
    end
    object Label8: TLabel
      Left = 16
      Top = 64
      Width = 40
      Height = 16
      Caption = 'Green:'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label9: TLabel
      Left = 24
      Top = 96
      Width = 30
      Height = 16
      Caption = 'Blue:'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label4: TLabel
      Left = 8
      Top = 240
      Width = 113
      Height = 16
      Caption = 'Valorile initiale: '
    end
    object EdRed: TLabel
      Left = 16
      Top = 272
      Width = 49
      Height = 24
      Alignment = taCenter
      AutoSize = False
      Color = clRed
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object EditRed: TLabel
      Left = 16
      Top = 128
      Width = 49
      Height = 24
      Alignment = taCenter
      AutoSize = False
      Color = clRed
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object EdGreen: TLabel
      Left = 88
      Top = 272
      Width = 49
      Height = 24
      Alignment = taCenter
      AutoSize = False
      Color = clMoneyGreen
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object EditGreen: TLabel
      Left = 88
      Top = 128
      Width = 49
      Height = 24
      Alignment = taCenter
      AutoSize = False
      Color = clMoneyGreen
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object EdBlue: TLabel
      Left = 160
      Top = 272
      Width = 49
      Height = 24
      Alignment = taCenter
      AutoSize = False
      Color = clSkyBlue
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object EditBlue: TLabel
      Left = 160
      Top = 128
      Width = 49
      Height = 24
      Alignment = taCenter
      AutoSize = False
      Color = clSkyBlue
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object scrGreen: TScrollBar
      Left = 72
      Top = 64
      Width = 121
      Height = 17
      PageSize = 0
      TabOrder = 0
      OnChange = scrGreenChange
    end
    object scrBlue: TScrollBar
      Left = 72
      Top = 96
      Width = 121
      Height = 17
      PageSize = 0
      TabOrder = 1
      OnChange = scrBlueChange
    end
    object scrRed: TScrollBar
      Left = 72
      Top = 32
      Width = 121
      Height = 17
      PageSize = 0
      TabOrder = 2
      OnChange = scrRedChange
    end
    object ButtonPrev: TButton
      Left = 16
      Top = 176
      Width = 49
      Height = 30
      Caption = 'Preview'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 3
      OnClick = ButtonPrevClick
    end
    object ButtonUndo: TButton
      Left = 72
      Top = 176
      Width = 40
      Height = 30
      Caption = 'Undo'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -11
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 4
      OnClick = ButtonUndoClick
    end
  end
  object GroupBox2: TGroupBox
    Left = 16
    Top = 453
    Width = 225
    Height = 161
    Caption = 'Properties'
    Color = clGray
    ParentColor = False
    TabOrder = 1
    Visible = False
    object Label1: TLabel
      Left = 15
      Top = 33
      Width = 66
      Height = 16
      Caption = 'File_name:'
      Color = clGray
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentColor = False
      ParentFont = False
    end
    object Label3: TLabel
      Left = 32
      Top = 73
      Width = 45
      Height = 16
      Caption = 'Height: '
      Color = clGray
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentColor = False
      ParentFont = False
    end
    object Label2: TLabel
      Left = 40
      Top = 113
      Width = 40
      Height = 16
      Caption = 'Width: '
      Color = clGray
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentColor = False
      ParentFont = False
    end
    object EditFile: TEdit
      Left = 88
      Top = 28
      Width = 121
      Height = 24
      BevelKind = bkFlat
      BevelOuter = bvSpace
      BiDiMode = bdRightToLeftNoAlign
      Enabled = False
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentBiDiMode = False
      ParentFont = False
      TabOrder = 0
      Text = 'EditFile'
    end
    object EditHeight: TEdit
      Left = 88
      Top = 68
      Width = 97
      Height = 24
      BevelKind = bkTile
      BevelOuter = bvSpace
      Enabled = False
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 1
      Text = 'EditHeight'
    end
    object EditWidth: TEdit
      Left = 88
      Top = 108
      Width = 73
      Height = 24
      BevelKind = bkTile
      BevelOuter = bvSpace
      Enabled = False
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlack
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentFont = False
      TabOrder = 2
      Text = 'EditWidth'
    end
  end
  object GroupBox3: TGroupBox
    Left = 456
    Top = 20
    Width = 121
    Height = 237
    Caption = 'Preview Bar'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlack
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 2
    Visible = False
    object ImageBefore: TImage
      Left = 8
      Top = 40
      Width = 110
      Height = 80
    end
    object ImageAfter: TImage
      Left = 8
      Top = 150
      Width = 110
      Height = 80
    end
    object StaticText1: TStaticText
      Left = 8
      Top = 20
      Width = 50
      Height = 20
      Caption = 'Before'
      Color = clAppWorkSpace
      ParentColor = False
      TabOrder = 0
    end
    object StaticText2: TStaticText
      Left = 8
      Top = 129
      Width = 36
      Height = 20
      Caption = 'After'
      TabOrder = 1
    end
    object stClose: TStaticText
      Left = 104
      Top = 12
      Width = 13
      Height = 20
      Caption = 'X'
      TabOrder = 2
      OnClick = stCloseClick
    end
  end
  object BtnReload: TButton
    Left = 592
    Top = 404
    Width = 57
    Height = 29
    Caption = 'Reload'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlack
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 3
    Visible = False
    OnClick = BtnReloadClick
  end
  object Chart1: TChart
    Left = 282
    Top = 452
    Width = 285
    Height = 170
    BackWall.Brush.Color = clWhite
    BackWall.Brush.Style = bsClear
    MarginBottom = 1
    MarginLeft = 1
    MarginRight = 1
    MarginTop = 1
    Title.AdjustFrame = False
    Title.Text.Strings = (
      'Histograma pe linie')
    LeftAxis.Visible = False
    Legend.Visible = False
    View3D = False
    TabOrder = 4
    Visible = False
    object Series1: TLineSeries
      Marks.ArrowLength = 8
      Marks.Style = smsValue
      Marks.Visible = False
      SeriesColor = clRed
      Title = 'Red'
      Pointer.HorizSize = 1
      Pointer.InflateMargins = True
      Pointer.Style = psCircle
      Pointer.VertSize = 1
      Pointer.Visible = False
      XValues.DateTime = False
      XValues.Name = 'X'
      XValues.Multiplier = 1
      XValues.Order = loAscending
      YValues.DateTime = False
      YValues.Name = 'Y'
      YValues.Multiplier = 1
      YValues.Order = loNone
    end
    object Series2: TLineSeries
      Marks.ArrowLength = 8
      Marks.Visible = False
      SeriesColor = clGreen
      Title = 'Green'
      Pointer.InflateMargins = True
      Pointer.Style = psRectangle
      Pointer.Visible = False
      XValues.DateTime = False
      XValues.Name = 'X'
      XValues.Multiplier = 1
      XValues.Order = loAscending
      YValues.DateTime = False
      YValues.Name = 'Y'
      YValues.Multiplier = 1
      YValues.Order = loNone
    end
    object Series3: TLineSeries
      Marks.ArrowLength = 8
      Marks.Visible = False
      SeriesColor = clBlue
      Title = 'Blue'
      Pointer.InflateMargins = True
      Pointer.Style = psRectangle
      Pointer.Visible = False
      XValues.DateTime = False
      XValues.Name = 'X'
      XValues.Multiplier = 1
      XValues.Order = loAscending
      YValues.DateTime = False
      YValues.Name = 'Y'
      YValues.Multiplier = 1
      YValues.Order = loNone
    end
  end
  object MainMenu1: TMainMenu
    Left = 992
    Top = 600
    object File1: TMenuItem
      Caption = 'File'
      object Open1: TMenuItem
        Caption = 'Open'
        ShortCut = 16463
        OnClick = Open1Click
      end
      object Save1: TMenuItem
        Caption = 'Save'
        Enabled = False
        ShortCut = 16467
        OnClick = Save1Click
      end
      object Exit2: TMenuItem
        Caption = '-'
      end
      object Exit3: TMenuItem
        Caption = 'Exit'
        OnClick = Exit3Click
      end
    end
    object Transformari1: TMenuItem
      Caption = 'Transformari geometrice'
      Visible = False
      object Zoom1: TMenuItem
        Caption = 'Zoom'
        object Zoomin1: TMenuItem
          Caption = 'Zoom in'
          ShortCut = 112
          OnClick = Zoomin1Click
        end
        object Zoomout1: TMenuItem
          Caption = 'Zoom out'
          ShortCut = 113
          OnClick = Zoomout1Click
        end
      end
      object Rotatia1: TMenuItem
        Caption = 'Rotatia'
        object N901: TMenuItem
          Caption = '90'
          ShortCut = 114
          OnClick = N901Click
        end
        object N1801: TMenuItem
          Caption = '180'
          ShortCut = 115
          OnClick = N1801Click
        end
      end
      object Translatia1: TMenuItem
        Caption = 'Translatia'
        object peverticala1: TMenuItem
          Caption = 'pe verticala'
          ShortCut = 116
          OnClick = peverticala1Click
        end
        object peorizontala1: TMenuItem
          Caption = 'pe orizontala'
          ShortCut = 117
          OnClick = peorizontala1Click
        end
      end
    end
    object Help1: TMenuItem
      Caption = 'Help'
      Visible = False
      object About1: TMenuItem
        Caption = 'About'
        OnClick = About1Click
      end
    end
  end
  object OpenDialog1: TOpenDialog
    Left = 256
    Top = 568
  end
  object SaveDialog1: TSaveDialog
    Left = 288
    Top = 568
  end
  object Timer1: TTimer
    Enabled = False
    OnTimer = Timer1Timer
    Left = 320
    Top = 568
  end
  object ImageList1: TImageList
    Left = 352
    Top = 568
    Bitmap = {
      494C010101000400040010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000400000001000000001002000000000000010
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000F0FBFF00F0FBFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000F0FBFF0000000000F0FBFF00F0FBFF00F0FB
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000F0FBFF00808060004060400000602000006040004060
      4000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000F0FBFF0000000000C0DCC0004060400080A0600040C0600040A060004060
      4000F0FBFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000F0FB
      FF0000000000C0DCC00080C0A0004060200080E0A00040C08000008040004080
      4000F0FBFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000C0DCC00080806000408040004060200080E0A00040C0600040A060004080
      4000F0FBFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C0C0
      C0004060600040604000406040004080400080E0A00040C0600040A060004060
      4000F0FBFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000004060
      6000C0C0C000C0C0C0004060400040804000C0DCC00080E0A00080C080000060
      2000F0FBFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000F0FBFF0080808000A4A0
      A000F0FBFF00A4A0A00040402000404020004060200040804000408060004080
      6000F0FBFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000F0FBFF0000000000806080000000
      0000C0C0A000C0C0C000808080000000000040806000C0DCC000F0FBFF008080
      8000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000F0FBFF0000000000F0FBFF008080
      80000000000080808000C0DCC00040604000A4A0A000F0FBFF00808080008060
      6000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000004040
      400000000000A4A0A000C0C0C000A4A0A000C0C0C000C0DCC00040604000A4A0
      A000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C0C0
      C000806060008060600080808000808080004040400080806000A4A0A0000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000F0FB
      FF0000000000F0FBFF004060400080606000F0FBFF0000000000F0FBFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000F0FB
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000F0FBFF00F0FBFF000000000000000000F0FB
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000424D3E000000000000003E000000
      2800000040000000100000000100010000000000800000000000000000000000
      000000000000000000000000FFFFFF00FF9F000000000000FE8F000000000000
      FC0F000000000000F407000000000000E807000000000000F007000000000000
      E007000000000000E0070000000000008007000000000000500F000000000000
      480F000000000000E80F000000000000E01F000000000000E85F000000000000
      FFEF000000000000FE6F00000000000000000000000000000000000000000000
      000000000000}
  end
end
