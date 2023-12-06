//This is a function that sets the text that will be saved and then displayed in your save data file
//This is called 3 times: to set the file number, set the player name and set the digimon species
//The file number uses a text size of 4, the player name a size of 13 and the digimon species a 17
//The player name is written in the own game special characters, which uses 2 bytes to render 1 character, the rest is just normal ASCII

char * SaveFileInfoData(char *storeTextPtr,char *textPtr,int textSize)
{
  int iVar1; 
  
  for (iVar1 = 0; (iVar1 < textSize && (*textPtr != 0)); textPtr = textPtr + 1)// It will stop reading if it has a 0, which makes it stop working early, this is the cause of the weird names
  {
    *storeTextPtr = *textPtr;
    iVar1 = iVar1 + 1;
    storeTextPtr = storeTextPtr + 1;
  }
  return storeTextPtr;
}


Disassembly:
                          SaveFileInfoData

        Offset       Hex         Commands
                         
        80112cf4 0a 00 00 10     b          0x80112d20
        80112cf8 21 38 00 00     _clear     a3
                             LAB_80112cfc                                   
        80112cfc 00 00 a3 80     lb         v1,0x0(a1)
        80112d00 00 00 00 00     nop
        80112d04 09 00 60 10     beq        v1,zero,0x80112d2c
        80112d08 00 00 00 00     _nop
        80112d0c 21 10 80 00     move       v0,a0
        80112d10 01 00 a5 24     addiu      a1,a1,0x1
        80112d14 01 00 44 24     addiu      a0,v0,0x1
        80112d18 00 00 43 a0     sb         v1,0x0(v0)
        80112d1c 01 00 e7 20     addi       a3,a3,0x1
                             LAB_80112d20                                   
        80112d20 2a 08 e6 00     slt        at,a3,a2
        80112d24 f5 ff 20 14     bne        at,zero,0x80112cfc
        80112d28 00 00 00 00     _nop
