//This patch should make the BGM keep playing rather than stop and start again each time you enter a new map but with the same BGM

Original:

void MapChange/StopScript(int scriptValue)
{
 
  if (scriptValue == 251) 
  {
    //code ignored
    JumpToStopMusic();  //It will set the old music data to -1, so the music function can override it
    //code ignored
  }
  return;
}


Changed:

void MapChange/StopScript(int scriptValue)
{
 
  if (scriptValue == 251) 
  {
    //code ignored
      //The "JumpToStopMusic" has been removed
    //code ignored
  }
  return;
}

Disassembly:

                        MapChange/StopScript

        Offset       Hex         Commands

Original:

        801028e8 3c 19 04 0c     jal        0x801064f0 //JumpToStopMusic


Changed:

        801028e8 00 00 00 00     nop





Original:

void PlayBGM(int trackID)

{
  byte local_2;
  byte local_1;
  
  local_2 = trackID;
  if (trackID == 0xff) 
    JumpToStopMusic();
  else 
  {
    FUN_80105558(&local_2,&local_1);  //This sets the font and track for the current song
    DAT_ffff94da = local_2;  //font
    DAT_ffff94db = local_1;  //track
    StopMusic();
    PlayMusic(local_2,local_1);
  }
  return;
}


Changed:

void PlayBGM(int trackID)

{
  byte local_2;
  byte local_1;
  
  local_2 = trackID;
  if (trackID == 0xff) 
    JumpToStopMusic();
  else 
  {
    FUN_80105558(&local_2,&local_1);  
    DAT_ffff94da = local_2;  
    DAT_ffff94db = local_1;  
    // The "StopMusic" has been removed
    PlayMusic(local_2,local_1);   //The play Music function already has a way to stop the music if the music has to change
  }
  return;
}


Disassembly:

                                PlayBGM

        Offset       Hex         Commands

Original:

        80106ac8 e1 19 03 0c     jal        0x800c6784 //StopMusic             


Changed:

        80106ac8 00 00 00 00     nop
