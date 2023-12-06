//This patch makes the save data stop displaying a weird name for your digimon species


Original:

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

Changed:

char * SaveFileInfoData(char *storeTextPtr,char *textPtr,int textSize)
{
  int iVar1;
  
  for (iVar1 = 0; iVar1 < textSize; textPtr = textPtr + 1) //I just removed the "&& (*textPtr != 0)", so it will overwrite all of the old text, there's no risk writing into unintended data since the size limit manages that. Writing "0" will not cause problems.
  {
    *storeTextPtr = *textPtr;
    iVar1 = iVar1 + 1;
    storeTextPtr = storeTextPtr + 1;
  }
  return storeTextPtr;
}


Disassembly:

        Offset       Hex         Commands

Original:
        80112d04 09 00 60 10     beq        v1,zero,0x80112d2c


Changed:
        80112d04 00 00 00 00     nop       

