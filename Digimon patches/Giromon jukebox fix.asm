//This is my own version of the giromon jukebox fix, it also fixes the "Mt. infinity" song name (originally named "Mt. Panorama")

//The error is caused by memory that should not be changed being overwritten due to an overflow. This happens if the amount of characters of a text that has to be laoded is above a certain amount.
//The game will corrupt the data and freeze. It can technically happen in a few places, but the jukebox is the only one that triggers it due to the long song names.

//For the original code, please check the repository

Changed:


//This function does not expands the limit anymore due to a lack of lines of code to fit that

Ptr* SetChoiceScrollListTextBox(Ptr*param_1,int CharLimit,int CharAmount)

{
  bool bVar1;
  int iVar2;
  
  if (CharLimit < CharAmount) //Check if the Character amount is smaller than the character limit to avoid errors
    CharAmount = CharLimit;
  
  iVar2 = CharLimit - CharAmount; //note that the division by 2 is not present anymore, so the limit now is reached earlier
  while (bVar1 = iVar2 != 0, iVar2 = iVar2 + -1, bVar1)
  {
    *param_1 = 0x81; 
    param_1[1] = 0x40;
    param_1 = param_1 + 2;
  }
  return param_1;
}

//Due to the lack of the limit expansion a few changes in the places where the function is called where made, these will be shown only in disassembly

//This is just an example to show the change

Original:
SetChoiceScrollListTextBox(param_1,12,CharAmount);

Changed:
SetChoiceScrollListTextBox(param_1,24,CharAmount);

Disassembly:

         Offset       Hex             Commands

                     SetChoiceScrollListTextBox

        800ff51c 43 10 06 00     sra        v0,a2,0x1
        800ff520 0a 00 00 10     b          0x800ff54c
        800ff524 22 30 a2 00     _sub       a2,a1,v0
                             LAB_800ff528                                   
        800ff528 21 10 80 00     move       v0,a0
        800ff52c 81 00 03 24     li         v1,0x81
        800ff530 00 00 43 a0     sb         v1,0x0(v0)
        800ff534 01 00 44 24     addiu      a0,v0,0x1
        800ff538 21 10 80 00     move       v0,a0
        800ff53c 40 00 03 24     li         v1,0x40
        800ff540 01 00 44 24     addiu      a0,v0,0x1
        800ff544 00 00 43 a0     sb         v1,0x0(v0)
        800ff548 ff ff c6 20     addi       a2,a2,-0x1
                             LAB_800ff54c                                   
        800ff54c f6 ff c0 14     bne        a2,zero,0x800ff528
        800ff550 00 00 00 00     _nop
        800ff554 08 00 e0 03     jr         ra
        800ff558 21 10 80 00     _move      v0,a0

//Extra code changes

Original:

        800fed2c 0c 00 05 24     li         a1,0xc

        800fee5c 0c 00 05 24     li         a1,0xc

        800ff048 08 00 05 24     li         a1,0x8

        8010aaf4 06 00 05 24     li         a1,0x6




Changed:

        800fed2c 18 00 05 24     li         a1,0x18

        800fee5c 18 00 05 24     li         a1,0x18

    Old //800ff048 10 00 05 24     li         a1,0x10
	New //800ff048 0C 00 05 24     li         a1,0xc
		

        8010aaf4 0c 00 05 24     li         a1,0xc



//Text changes to fit better the boxes


//This is a the pointer to the box that plays "Mt. Infinity" music, it has been changed to the new data, the old one just points to "Mt. Panorama"
14D721C4 3C F7 12 80 to E8 FB 12 80


//The changes inside this last sections are different in Maeson's hack, it was just text changes that are pretty easy to do

//Text changes as text

//Update: all of the themes have been removed from the names

"Non Bewildering Forest Theme" to "Native Forest"
"Non Bewildering Forest Night Theme" to "Native Forest Night"
"Ogremon Theme No. 2" to "Ogremon 2"
"Everything Shop Theme" to "Monocromon Shop"
"Ogremon ThemeNo. 3" to "Ogremon 3"
"Dark Aristcrat's Mansion Theme" to "Grey Lord Mansion"
"The Ancient Region of Dino Speedy Time Zone Theme" to "A. Dino Speed " 
"The Ancient Region of Dino Speedy Time Zone Night Theme" to "A. Dino Speed Night"
"The Ancient Region of Dino Glacial Time Zone Theme" to "A. Dino Glacial"
"The Ancient Region of Dino Glacial Time Zone Night Theme" to "A. Dino Glacial Night"
"Sanctuary Below Theme" to "Sanctuary Underground" //Just a personal change
"Theme of Factorial" to "Factorial Town Day"
"Factorial Night Theme" to "Factorial Town Night" 
"Beatland Theme" to "Bettle Land Day" 
"Beatland Night Theme" to "Bettle Land Night" 
"Tornament Opening Theme" to "Tournament Start"
"Tornament Progress Theme" to "Tournament Progress"
"Tornament Championship Theme" to "Tournament Champion"
"Partner's Entrance Theme" to "Partner Entrance" //This is just to match the next one
"Competition Battle Opponent's Entrance Theme" to "Opponent Entrance" 
"Mt. Infinity Theme" Added around here
"Arena Battle Theme No. 1" to "Arena Battle 1"
"Partner's Win Theme" to "Partner Win" //match the previous ones
"Partner's Loss Theme" to "Partner Loss" //match the previous ones
"Arena Battle Theme No. 2" to "Arena Battle 2"
"Arena Battle Theme No. 3" to "Arena Battle 3"
"Normal Battle Theme" to "Hard Battle"
"Normal Battle  Theme No.2" to "Normal Battle"
"Last Battle Theme" to "Last Battle"
 


//Text changes as Hex

14D717E8 to 14D71917

It is easy to see the text with a Hex editor
