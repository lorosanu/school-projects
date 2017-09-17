//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "jpeg.hpp"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
        ClientHeight =  676;
        ClientWidth = 812;
}
//---------------------------------------------------------------------------

int PeVerticala = 0, PeOrizontala = 0;
int pas = 0;
Graphics::TBitmap *Sursa = new Graphics::TBitmap;
Byte R[550][450], G[550][450], B[550][450];


void __fastcall TForm1::Open1Click(TObject *Sender)
{
        OpenDialog1->Filter = "JPEG images|*.jpg;| Bmp images|*.BMP" ;

        if (OpenDialog1->Execute())
        {
            if (!FileExists(OpenDialog1->FileName))
                return;
            AnsiString temp2 = ExtractFileName(OpenDialog1->FileName);
            AnsiString temp = ExtractFileExt(OpenDialog1->FileName);
            AnsiString Ext = temp.LowerCase();

            if (Ext.AnsiPos("jpg") > 0)  // it's a jpg
            {
                TJPEGImage *myjpeg = new TJPEGImage();
                myjpeg->LoadFromFile(OpenDialog1->FileName);
                myjpeg->DIBNeeded();
                Image1->Picture->Bitmap->Assign(myjpeg);
                delete myjpeg;
            }
            else
                if (Ext.AnsiPos("bmp") > 0)
                         Image1->Picture->Bitmap->LoadFromFile(OpenDialog1->FileName);

            Transformari1->Visible = true;
            Save1->Enabled = true;
            Help1->Visible = true;

            Sursa = new Graphics::TBitmap;
            Sursa->Assign( Image1->Picture->Bitmap );
            Sursa->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

            EditFile->Text = ExtractFileName(OpenDialog1->FileName);
            EditWidth->Text = Image1->Width;
            EditHeight->Text = Image1->Height;
            GroupBox2->Visible= true;


            Graphics::TBitmap *source = new Graphics::TBitmap;
            source->Assign( Image1->Picture->Bitmap );
            source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

            int r=0, g=0, b=0;

            for (int x=0; x<source->Width; x++ )
                for(int y=0; y<source->Height; y++ )
                {
                   Byte red=(Byte)(GetRValue(source->Canvas->Pixels[x][y]));
                   Byte green =(Byte)(GetGValue(source->Canvas->Pixels[x][y]));
                   Byte blue=(Byte)(GetBValue(source->Canvas->Pixels[x][y]));
                   R[x][y]= red;
                   G[x][y]= green;
                   B[x][y]= blue;
                   r+=red;
                   g+=green;
                   b+=blue;
                }

        r = r/(source->Width * source->Height);
        b = b/(source->Width * source->Height);
        g = g/(source->Width * source->Height);

        EdRed->Caption= r;
        EdGreen->Caption = g;
        EdBlue->Caption =b;

        EditRed->Caption = 0;
        EditGreen->Caption = 0;
        EditBlue->Caption = 0;

        GroupBox1->Visible = true;

        BtnReload->Visible = true;
   }
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Exit3Click(TObject *Sender)
{
        Form1->Close();
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Save1Click(TObject *Sender)
{
       if( Image1->Picture->Bitmap->Empty == true )
                return;
        SaveDialog1->Title = "Save Image";
        SaveDialog1->DefaultExt = "jpg";
        SaveDialog1->Filter = "JPEG images (*.jpg) | *.jpg; | Bmp files (*.bmp)|*.BMP" ;
        SaveDialog1->FilterIndex = 1;
        if (SaveDialog1->Execute())
        {
            AnsiString temp2 = ExtractFileName(SaveDialog1->FileName);
            AnsiString temp = ExtractFileExt(SaveDialog1->FileName);
            AnsiString Ext = temp.LowerCase();
            if (Ext.AnsiPos("jpg") > 0)
            {
                TJPEGImage *jp = new TJPEGImage();
                try
                {
                  jp->Assign(Image1->Picture->Bitmap);
                  jp->SaveToFile(SaveDialog1->FileName);
                }
                __finally
                {
                  delete jp;
                }
            }
            else  if (Ext.AnsiPos("bmp") > 0)
            {
               Image1->Picture->Bitmap->SaveToFile(SaveDialog1->FileName);
            }
        }  
   
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Image1MouseMove(TObject *Sender, TShiftState Shift,
      int X, int Y)
{
    if (GroupBox2->Visible ==true)
    {
        Graphics::TBitmap *source = new Graphics::TBitmap;
        Image1->Stretch = true;
        source->Assign( Image1->Picture->Bitmap );
        source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

        Graphics::TBitmap *dest = new Graphics::TBitmap;
        dest->Width = source->Width*2;
        dest->Height = source->Height*2;
        dest->PixelFormat = source->PixelFormat;

        int y2=0, x2=0, xs, xf, ys, yf;

        for (int i=X-40; i<X+40; i++)
        {
           y2=0;
           for(int j=Y-40; j<Y+40; j++)
           {
               if (i>=0 && i<=source->Width-1 && j>=0 && j<=source->Height-1)
               {
                        Byte red=(Byte)((GetRValue(source->Canvas->Pixels[i][j])+GetRValue(source->Canvas->Pixels[i][j+1]))/2);
                        Byte green=(Byte)((GetGValue(source->Canvas->Pixels[i][j])+GetGValue(source->Canvas->Pixels[i][j+1]))/2);
                        Byte blue=(Byte)((GetBValue(source->Canvas->Pixels[i][j])+GetBValue(source->Canvas->Pixels[i][j+1]))/2);

                        Byte red1=(Byte)((GetRValue(source->Canvas->Pixels[i][j])+GetRValue(source->Canvas->Pixels[i+1][j]))/2);
                        Byte green1=(Byte)((GetGValue(source->Canvas->Pixels[i][j])+GetGValue(source->Canvas->Pixels[i+1][j]))/2);
                        Byte blue1=(Byte)((GetBValue(source->Canvas->Pixels[i][j])+GetBValue(source->Canvas->Pixels[i+1][j]))/2);

                        dest->Canvas->Pixels[x2][y2] = source->Canvas->Pixels[i][j];
                        dest->Canvas->Pixels[x2][y2+1] = TColor(RGB(red,green,blue));
                        dest->Canvas->Pixels[x2+1][y2] = TColor(RGB(red1,green1,blue1));

                        Byte red2=(Byte)((GetRValue(dest->Canvas->Pixels[x2][y2+1])+GetRValue(source->Canvas->Pixels[i][j+1])+(GetRValue(dest->Canvas->Pixels[x2+1][y2])+GetRValue(source->Canvas->Pixels[i+1][j+1])))/4);
                        Byte green2=(Byte)((GetGValue(dest->Canvas->Pixels[x2][y2+1])+GetGValue(source->Canvas->Pixels[i][j+1])+(GetGValue(dest->Canvas->Pixels[x2+1][y2])+GetGValue(source->Canvas->Pixels[i+1][j+1])))/4);
                        Byte blue2=(Byte)(Byte)((GetBValue(dest->Canvas->Pixels[x2][y2+1])+GetBValue(source->Canvas->Pixels[i][j+1])+(GetBValue(dest->Canvas->Pixels[x2+1][y2])+GetBValue(source->Canvas->Pixels[i+1][j+1])))/4);

                        dest->Canvas->Pixels[x2+1][y2+1] =TColor(RGB(red2,green2,blue2));
               }
              y2=y2+2;
           }
        x2=x2+2;
      }


        RGBTRIPLE* pixels;
       	TColor color;

       	Chart1->Series[0]->Clear();
        pixels = (RGBTRIPLE*)source->ScanLine[Y];
        for (int x=0; x<source->Width; x++)
            Chart1->Series[0]->AddY( (int)(pixels[x].rgbtRed + pixels[x].rgbtBlue + pixels[x].rgbtGreen )/3, "", clGray);

        Chart1->Series[0]->Active = true;
        Chart1->Visible = true;

        Image2->Picture->Bitmap = dest;
        delete dest; delete source;
    }
}
//---------------------------------------------------------------------------



void __fastcall TForm1::Zoomout1Click(TObject *Sender)
{
    int y2,x2;

    Graphics::TBitmap *source = new Graphics::TBitmap;
    source->Assign( Image1->Picture->Bitmap );
    source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

    if (source->Width >=150)
    {
        Graphics::TBitmap *dest = new Graphics::TBitmap;
        dest->Width = source->Width/2;
        dest->Height = source->Height/2;
        dest->PixelFormat = source->PixelFormat;

        y2=x2=0;
        for (int i=0; i<source->Width; i=i+2 )
        {
          y2=0;
          for(int j=0; j<source->Height; j=j+2)
          {
             dest->Canvas->Pixels[x2][y2] =source->Canvas->Pixels[i][j];
             y2=y2+1;
          }
          x2=x2+1;
        }

        Image1->Picture->Bitmap = dest;
        delete dest; delete source;

        EditWidth->Text = Image1->Width;
        EditHeight->Text = Image1->Height;
    }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Zoomin1Click(TObject *Sender)
{
     Graphics::TBitmap *source = new Graphics::TBitmap;
     source->Assign( Image1->Picture->Bitmap );
     source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

     if (source->Width <500)
     {
        Graphics::TBitmap *dest = new Graphics::TBitmap;
        dest->Width = source->Width*2;
        dest->Height = source->Height*2;
        dest->PixelFormat = source->PixelFormat;

        int x2=0;
        int y2=0;
        RGBTRIPLE *pixels;

        for (int y=0; y<source->Height; y++)
        {
                x2=0;
                pixels = (RGBTRIPLE *)source->ScanLine[y];
                for (int x=0; x<source->Width; x++)
                {
                        dest->Canvas->Pixels[x2][y2]=TColor(RGB(pixels[x].rgbtRed,pixels[x].rgbtGreen,pixels[x].rgbtBlue));
                        dest->Canvas->Pixels[x2][y2+1]=TColor(RGB(pixels[x].rgbtRed, pixels[x].rgbtGreen, pixels[x].rgbtBlue));
                        dest->Canvas->Pixels[x2+1][y2]=TColor(RGB(pixels[x].rgbtRed, pixels[x].rgbtGreen, pixels[x].rgbtBlue));
                        dest->Canvas->Pixels[x2+1][y2+1]=TColor(RGB(pixels[x].rgbtRed, pixels[x].rgbtGreen, pixels[x].rgbtBlue));
                        x2=x2+2;       
                }
                y2=y2+2;
        }

       Image1->Picture->Bitmap = dest;
       delete dest; delete source;

        EditWidth->Text = Image1->Width;
        EditHeight->Text = Image1->Height;
     }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::N901Click(TObject *Sender)
{
      Graphics::TBitmap *source = new Graphics::TBitmap;
      source->Assign( Image1->Picture->Bitmap );
      source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

      Graphics::TBitmap *dest = new Graphics::TBitmap;
      dest->Width = source->Height;
      dest->Height = source->Width;
      dest->PixelFormat = source->PixelFormat;


      for (int x=0; x<source->Width; x++ )
        for(int y=0; y<source->Height; y++ )
            dest->Canvas->Pixels[y][source->Width-1-x] = source->Canvas->Pixels[x][y];


      Image1->Picture->Bitmap = dest;
      delete dest; delete source;

        EditWidth->Text = Image1->Width;
        EditHeight->Text = Image1->Height;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::N1801Click(TObject *Sender)
{
       Graphics::TBitmap *source = new Graphics::TBitmap;
       source->Assign( Image1->Picture->Bitmap );
       source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

       Graphics::TBitmap *dest = new Graphics::TBitmap;
       dest->Width = source->Width;
       dest->Height = source->Height;
       dest->PixelFormat = source->PixelFormat;

       for (int x=0;x<source->Width;x++)
          for(int y=0;y<source->Height;y++)
              dest->Canvas->Pixels[x][source->Height-1-y]=source->Canvas->Pixels[x][y];

       Image1->Picture->Bitmap=dest;
       delete dest; delete source;

       EditWidth->Text = Image1->Width;
       EditHeight->Text = Image1->Height;       
}
//---------------------------------------------------------------------------

void __fastcall TForm1::peverticala1Click(TObject *Sender)
{
       PeOrizontala=0;
       PeVerticala=1;
       pas=0;
       Timer1->Enabled = true;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::peorizontala1Click(TObject *Sender)
{
       PeVerticala=0;
       PeOrizontala=1;
       pas=0;
       Timer1->Enabled = true;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::Timer1Timer(TObject *Sender)
{
      if (PeVerticala == 1)
      {
         int y2,x2;
         pas++;
         
         Graphics::TBitmap *source = new Graphics::TBitmap;
         source->Assign( Image1->Picture->Bitmap );
         source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

         Graphics::TBitmap *dest = new Graphics::TBitmap;
         dest->Width = source->Width;
         dest->Height = source->Height;
         dest->PixelFormat = source->PixelFormat;

         y2=source->Height/4;
         for(int y=0;y<source->Height;y++)
         {
            x2=0;
            for(int x=0;x<source->Width;x++)
            {
                dest->Canvas->Pixels[x2][y2]=source->Canvas->Pixels[x][y];
                x2=x2+1;
            }
            if (y2==source->Height-1)
                 y2=-1;
            y2=y2+1;
        }

        Image1->Picture->Bitmap = dest;
        delete dest; delete source;

        if (pas==4)
                Timer1->Enabled = false;
      }
      else
        if (PeOrizontala == 1)
        {
          int y2,x2;
          pas++;

          Graphics::TBitmap *source = new Graphics::TBitmap;
          source->Assign( Image1->Picture->Bitmap );
          source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

          Graphics::TBitmap *dest = new Graphics::TBitmap;
          dest->Width = source->Width;
          dest->Height = source->Height;
          dest->PixelFormat = source->PixelFormat;

          y2=0;
          for(int y=0;y<source->Height;y++)
          {
             x2=source->Width/4;
             for(int x=0;x<source->Width;x++)
             {
                 dest->Canvas->Pixels[x2][y2]=source->Canvas->Pixels[x][y];
                 if (x2==source->Width-1)
                       x2=-1;
                 x2=x2+1;
             }
             y2=y2+1;
         }

         Image1->Picture->Bitmap = dest;
         delete dest; delete source;

         if (pas==4)
            Timer1->Enabled = false;
        }
}
//---------------------------------------------------------------------------

void __fastcall TForm1::scrRedChange(TObject *Sender)
{
        scrGreen->Position = 0;
        scrBlue->Position = 0;
        Graphics::TBitmap *dest = new Graphics::TBitmap;
        dest->Width = Sursa->Width;
        dest->Height = Sursa->Height;
        dest->PixelFormat = Sursa->PixelFormat;
        byte r;
        int s=0;

        for (int i=0; i<Sursa->Width; i++)
           for(int j=0; j<Sursa->Height; j++)
           {
                if (R[i][j]<128)
                     r = (R[i][j] * scrRed->Position )/ 50;
                else
                     r = (255 * scrRed->Position) / 100;
             s+=r;
             dest->Canvas->Pixels[i][j] = TColor(RGB(r,G[i][j],B[i][j]));
          }

        s=s/(Sursa->Height * Sursa->Width);
        EditRed->Caption = s;
        Image1->Picture->Bitmap = dest;
        delete dest;

}
//---------------------------------------------------------------------------

void __fastcall TForm1::scrGreenChange(TObject *Sender)
{
        scrRed->Position = 0;
        scrBlue->Position = 0;
        Graphics::TBitmap *dest = new Graphics::TBitmap;
        dest->Width = Sursa->Width;
        dest->Height = Sursa->Height;
        dest->PixelFormat = Sursa->PixelFormat;
        byte g;
        int s=0;

        for (int i=0; i<Sursa->Width; i++)
           for(int j=0; j<Sursa->Height; j++)
           {
                if (G[i][j]<128)
                     g = (G[i][j] * scrGreen->Position )/ 50;
                else
                     g = (255 * scrGreen->Position) / 100;
             s+=g;
             dest->Canvas->Pixels[i][j] = TColor(RGB(R[i][j],g,B[i][j]));
          }

        s=s/(Sursa->Height * Sursa->Width);
        EditGreen->Caption = s;
        Image1->Picture->Bitmap = dest;
        delete dest;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::scrBlueChange(TObject *Sender)
{
        scrRed->Position = 0;
        scrGreen->Position = 0;
        Graphics::TBitmap *dest = new Graphics::TBitmap;
        dest->Width = Sursa->Width;
        dest->Height = Sursa->Height;
        dest->PixelFormat = Sursa->PixelFormat;
        byte b;
        int s=0;

        for (int i=0; i<Sursa->Width; i++)
           for(int j=0; j<Sursa->Height; j++)
           {
                if (B[i][j]<128)
                     b = (B[i][j] * scrBlue->Position )/ 50;
                else
                     b = (255 * scrBlue->Position) / 100;
             s+=b;
             dest->Canvas->Pixels[i][j] = TColor(RGB(R[i][j],G[i][j],b));
          }

        s=s/(Sursa->Height * Sursa->Width);
        EditBlue->Caption = s;
        Image1->Picture->Bitmap = dest;
        delete dest;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::ButtonPrevClick(TObject *Sender)
{
   if(GroupBox3->Visible == false)
   {
        int nro=5;
        Graphics::TBitmap *destb = new Graphics::TBitmap;
        destb->Width = Sursa->Width/nro;
        destb->Height = Sursa->Height/nro;
        destb->PixelFormat = Sursa->PixelFormat;

        int x2, y2;

        y2=x2=0;
        for (int i=0; i<Sursa->Width; i=i+5 )
        {
          y2=0;
          for(int j=0; j<Sursa->Height; j=j+5)
          {
             destb->Canvas->Pixels[x2][y2] = Sursa->Canvas->Pixels[i][j];;
             y2=y2+1;
          }
          x2=x2+1;
        }

        ImageBefore->Picture->Bitmap = destb;
        delete destb;

        Graphics::TBitmap *source = new Graphics::TBitmap;
        source->Assign( Image1->Picture->Bitmap );
        source->PixelFormat = Image1->Picture->Bitmap->PixelFormat;

        Graphics::TBitmap *desta = new Graphics::TBitmap;
        desta->Width = source->Width/nro;
        desta->Height = source->Height/nro;
        desta->PixelFormat = source->PixelFormat;


        y2=x2=0;
        for (int i=0; i<source->Width; i=i+5 )
        {
          y2=0;
          for(int j=0; j<source->Height; j=j+5)
          {
             desta->Canvas->Pixels[x2][y2] =source->Canvas->Pixels[i][j];
             y2=y2+1;
          }
          x2=x2+1;
        }

        ImageAfter->Picture->Bitmap = desta;
        delete desta; delete source;
   }

   GroupBox3->Visible = !GroupBox3->Visible;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::stCloseClick(TObject *Sender)
{
        GroupBox3->Visible = false;        
}
//---------------------------------------------------------------------------


void __fastcall TForm1::ButtonUndoClick(TObject *Sender)
{
        scrRed->Position = 0;
        scrGreen->Position = 0;
        scrBlue->Position=0;
        Image1->Picture->Bitmap = Sursa;
}
//---------------------------------------------------------------------------

void __fastcall TForm1::BtnReloadClick(TObject *Sender)
{
      Image1->Picture->Bitmap = Sursa;
      EditWidth->Text = Image1->Width;
      EditHeight->Text = Image1->Height;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::About1Click(TObject *Sender)
{
        ShowMessage("Made by Orosanu Luiza\n1131A");
}
//---------------------------------------------------------------------------






