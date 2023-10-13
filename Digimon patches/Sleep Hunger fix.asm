//This is a patch that fixes the sleep math error that causes a digimon to not be able to show hunger for a day, this patch was originally made to help Maeson with his hack
//The glitch itself is pretty long to explain, so I may just make a wiki page about it

// If you are interested in the full original function, SydMontague has the code on Github

Original:
  
// code ignored
    CurrentHour = (int)_CurrentHourMemory;
    int currentLoop = 1;
    CurrentDigimonHungerPointer = DigimonType * 28;
    for (count = 0; count < 8; count = count + 1) 
    {
      if (HungerTime < CurrentHour)
      {
        if (CurrentHour < (&HungerTimesPtr)[count + CurrentDigimonHungerPointer])
        {
          HungerTime = (&HungerTimesPtr)[count + DigimonType * 28];
          break;
        }
      }
      else if (HungerTime < (&HungerTimesPtr)[count + CurrentDigimonHungerPointer]) //This is the reason for the error
      {
        HungerTime = (&HungerTimesPtr)[count + DigimonType * 28];
        break;
      }
      if (((&HungerTimesPtr)[currentLoop + CurrentDigimonHungerPointer] == -1) || (count == 7))
     {
        count = 0;
        currentLoop = 1;
        goto LAB_800a4c98;
      }
      currentLoop = currentLoop + 1;
    }
  //code ignored


Changed:

void SetFoodTimer(int DigimonType)
{

//code ignored

    CurrentHour = (int)_CurrentHourMemory;
    int currentLoop = 1;
    CurrentDigimonHungerPointer = DigimonType * 28;
    for (count = 0; count < 8; count = count + 1) 
    {
      if (HungerTime < CurrentHour)
      {
        if (CurrentHour < (char)(&HungerTimesPtr)[count + CurrentDigimonHungerPointer])
        {
          HungerTime = (short)(char)(&HungerTimesPtr)[count + DigimonType * 28];
          break;
        }
LAB_800a4bbc:
        puVar2 = &HungerTimesPtr + CurrentDigimonHungerPointer;
      }
      else 
      {
        puVar2 = (int)(char)(&HungerTimesPtr)[count + CurrentDigimonHungerPointer];

        if ((int)puVar2 <= (int)HungerTime) goto LAB_800a4bbc; // if the current selected time is lower than the old selected time, just keep looping

        if ((int)puVar2 - CurrentHour < 13) // checks if the difference between the current selected time and the in game time is bigger than 12, which should never happen, if it is bigger, just keep looping to trigger the failsafe
        {
          HungerTime = (short)(char)(&HungerTimesPtr)[count + CurrentDigimonHungerPointer];
          break;
        }
      }
      if ((puVar2[currentLoop] == -1) || (count == 7)) 
      {
        count = 0;
        currentLoop = 1;
        goto LAB_800a4c98;
      }
      currentLoop = currentLoop + 1;
    }
  

//code ignored
}


//The actual change is pretty small

Disassembly:

         Offset      Hex               Commands

Original:
        800a4b50 c0 20 02 00     sll        a0,v0,0x3
        800a4b54 22 10 82 00     sub        v0,a0,v0
        800a4b58 80 10 02 00     sll        v0,v0,0x2
        800a4b5c 21 10 82 01     addu       v0,t4,v0
        800a4b60 21 10 62 00     addu       v0,v1,v0
        800a4b64 00 00 42 80     lb         v0,0x0(v0)

Changed:
        800a4b50 22 08 69 01     sub        at,t3,t1
        800a4b54 0d 00 21 28     slti       at,at,0xd
        800a4b58 1b 00 20 10     beq        at,zero,0x800a4bc8
        800a4b5c 00 00 00 00     _nop
        800a4b60 00 00 00 00     nop
        800a4b64 00 00 00 00     nop
                        
