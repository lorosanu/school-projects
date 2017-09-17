//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Chart.hpp>
#include <Dialogs.hpp>
#include <ExtCtrls.hpp>
#include <ImgList.hpp>
#include <Menus.hpp>
#include <Series.hpp>
#include <TeEngine.hpp>
#include <TeeProcs.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
        TImage *Image1;
        TImage *Image2;
        TSplitter *Splitter1;
        TGroupBox *GroupBox1;
        TLabel *Label5;
        TLabel *Label8;
        TLabel *Label9;
        TLabel *Label4;
        TLabel *EdRed;
        TLabel *EditRed;
        TLabel *EdGreen;
        TLabel *EditGreen;
        TLabel *EdBlue;
        TLabel *EditBlue;
        TScrollBar *scrGreen;
        TScrollBar *scrBlue;
        TScrollBar *scrRed;
        TButton *ButtonPrev;
        TButton *ButtonUndo;
        TGroupBox *GroupBox2;
        TLabel *Label1;
        TLabel *Label3;
        TLabel *Label2;
        TEdit *EditFile;
        TEdit *EditHeight;
        TEdit *EditWidth;
        TGroupBox *GroupBox3;
        TImage *ImageBefore;
        TImage *ImageAfter;
        TStaticText *StaticText1;
        TStaticText *StaticText2;
        TStaticText *stClose;
        TButton *BtnReload;
        TChart *Chart1;
        TLineSeries *Series1;
        TLineSeries *Series2;
        TLineSeries *Series3;
        TMainMenu *MainMenu1;
        TMenuItem *File1;
        TMenuItem *Open1;
        TMenuItem *Save1;
        TMenuItem *Exit2;
        TMenuItem *Exit3;
        TMenuItem *Transformari1;
        TMenuItem *Zoom1;
        TMenuItem *Zoomin1;
        TMenuItem *Zoomout1;
        TMenuItem *Rotatia1;
        TMenuItem *N901;
        TMenuItem *N1801;
        TMenuItem *Translatia1;
        TMenuItem *peverticala1;
        TMenuItem *peorizontala1;
        TMenuItem *Help1;
        TMenuItem *About1;
        TOpenDialog *OpenDialog1;
        TSaveDialog *SaveDialog1;
        TTimer *Timer1;
        TImageList *ImageList1;
        void __fastcall Open1Click(TObject *Sender);
        void __fastcall Exit3Click(TObject *Sender);
        void __fastcall Save1Click(TObject *Sender);
        void __fastcall Image1MouseMove(TObject *Sender, TShiftState Shift,
          int X, int Y);
        void __fastcall Zoomout1Click(TObject *Sender);
        void __fastcall Zoomin1Click(TObject *Sender);
        void __fastcall N901Click(TObject *Sender);
        void __fastcall N1801Click(TObject *Sender);
        void __fastcall peverticala1Click(TObject *Sender);
        void __fastcall peorizontala1Click(TObject *Sender);
        void __fastcall Timer1Timer(TObject *Sender);
        void __fastcall scrRedChange(TObject *Sender);
        void __fastcall scrGreenChange(TObject *Sender);
        void __fastcall scrBlueChange(TObject *Sender);
        void __fastcall ButtonPrevClick(TObject *Sender);
        void __fastcall stCloseClick(TObject *Sender);
        void __fastcall ButtonUndoClick(TObject *Sender);
        void __fastcall BtnReloadClick(TObject *Sender);
        void __fastcall About1Click(TObject *Sender);
private:	// User declarations
public:		// User declarations
        __fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
