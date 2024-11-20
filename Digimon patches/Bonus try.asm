//This code is to show all the version of the bonus try patches
//The original code can be found in SydMontague github, under the name of "doSlotsRigging.asm"



//This is the slots fix

void SlotsHelp(int currentSlot,int slotDataPtr)
{
  int iVar1;
  uint uVar2;
  char cVar3;
  
  if (currentSlot == 2) 
  {
    helpType = currentHelp;
    if (helpType != 2)  //Gold poop help
    {
      if (helpType == 1) //normal help
      {
        
        if (Slot0 != Slot1) 
          return;
        
        if (cVar3 != 7)  //is not gold poop
        {
         //code ignored
          return;
        }
        else
        {
          return;
        }
        
     }
     if (helpType == 0) 
       return;

   //code ignored
     
  }

//code ignored
}

Disassembly:

         Offset       Hex         Commands

                              SlotsHelp
Original:
        8008ebf0 2d 00 61 10     beq        v1,at,LAB_8008eca8


        8008eb40 83 00 40 14     bne        v0,zero,LAB_8008ed50


Changed:
        8008ebf0 57 00 61 10     beq        v1,at,LAB_8008ed50


        8008eb40 83 00 40 10     beq        v0,zero,LAB_8008ed50







//This is the super helpful patch, it includes the change from earlier

void SlotsHelp(int currentSlot,int slotDataPtr)
{
  int iVar1;
  uint uVar2;
  char cVar3;
  
  if (currentSlot == 2) 
  {
    helpType = currentHelp;
    if (helpType != 2)  //Gold poop help
    {
      if (helpType == 1) //normal help
      {
        
        if (Slot0 != Slot1) 
          return;
        
        if (Slot2 != 7) //is not gold poop
        {
          iVar1 = 1;
          while (iVar1 < 14 && (Slot1 != (&SlotsValues3)[(currentSlotValue + 13 - iVar1) % 13]))  //Now it will try to do a full loop to the slots values        
            iVar1++;

        if ((iVar1 != 14) && (iVar1 != 0)) 
        {
          //code ignored
        }
          
          //code ignored
          return;
        }
        else
        {
          return;
        }
        
     }
     if (helpType == 0) 
       return;
    //code ignored
  }
  else  if (SlotValue == 1) {
       //code ignored
      if (Slot1 != 7) 
      {
        iVar1 = 1;
        while (iVar1 < 14 && (Slot0 != (&SlotsValues2)[(currentSlotValue + 13 - iVar1) % 13]))  //Now it will try to do a full loop to the slots values 
          iVar1++;
        if ((iVar1 != 14) && (iVar1 != 0)) 
        {
          //code ignored
        }
        //code ignored
      }
    // code ignored
    }

//code ignored
}

Disassembly:

         Offset       Hex         Commands

                              SlotsHelp
Original:
        8008ec5c 03 00 41 28     slti       at,v0,0x3
 
        8008ec68 03 00 01 24     li         at,0x3


        8008eab8 03 00 41 28     slti       at,v0,0x3

        8008eac4 03 00 01 24     li         at,0x3
		
		
        8008e994 03 00 41 28     slti       at,v0,0x3 //I forgot to add these two to the document.

        8008e9a0 03 00 01 24     li         at,0x3



Changed:
        8008ec5c 0d 00 41 28     slti       at,v0,0xe  //updated value, so it never fails
 
        8008ec68 0d 00 01 24     li         at,0xe

        8008eab8 0d 00 41 28     slti       at,v0,0xe

        8008eac4 0d 00 01 24     li         at,0xe
		
        8008e994 03 00 41 28     slti       at,v0,0xe

        8008e9a0 03 00 01 24     li         at,0xe




// Ultra Lucky patch

This is just some data changes in 14C8E6D4, which are used to the the type of help, it also includes all of the earlier changes

Original:
00 00 00 00 00 01 01 01 01 02

Changed:
00 01 01 01 01 01 01 02 02 02