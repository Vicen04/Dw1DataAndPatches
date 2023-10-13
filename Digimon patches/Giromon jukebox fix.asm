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

        800ff048 10 00 05 24     li         a1,0x10

        8010aaf4 0c 00 05 24     li         a1,0xc



//Text changes to fit better the boxes


//This is a the pointer to the box that plays "Mt. Infinity" music, it has been changed to the new data, the old one just points to "Mt. Panorama"
14D721C4 3C F7 12 80 to E8 FB 12 80


//The changes inside this last sections are different in Maeson's hack, it was just text changes that are pretty easy to do

//Text changes as text

"Non Bewildering Forest Theme" to "Native Forest Theme"
"Non Bewildering Forest Night Theme" to "Native Forest Night Theme"
"Ogremon Theme No. 2" to "Ogremon Theme 2"
"Everything Shop Theme" to "Monocromon Shop Theme"
"Ogremon ThemeNo. 3" to "Ogremon Theme 3"
"Dark Aristcrat's Mansion Theme" to "Grey Lord Mansion Theme"
"The Ancient Region of Dino Speedy Time Zone Theme" to "Dino Speed Theme" 
"The Ancient Region of Dino Speedy Time Zone Night Theme" to "Dino Speed Night Theme"
"The Ancient Region of Dino Glacial Time Zone Theme" to "Dino Glacial Theme"
"The Ancient Region of Dino Glacial Time Zone Night Theme" to "Dino Glacial Night Theme"
"Sanctuary Below Theme" to "Sanctuary Secret Theme" //Just a personal change
"Theme of Factorial" to "Factorial Town Day" //The *Theme* was ommited due to a lack of space
"Factorial Night Theme" to "Factorial Town Night" //The *Theme* was ommited due to a lack of space
"Beatland Theme" to "Bettle Land Day" //The *Theme* was ommited due to a lack of space
"Beatland Night Theme" to "Bettle Land Night" //The *Theme* was ommited to match the day theme
"Tornament Opening Theme" to "Tournament Start Theme"
"Tornament Progress Theme" to "Tournament Progress Theme"
"Tornament Championship Theme" to "Tournament Champion Theme"
"Partner's Entrance Theme" to "Partner Entrance Theme" //This is just to match the next one
"Competition Battle Opponent's Entrance Theme" to "Opponent Entrance Theme" 
"Mt. Infinity Theme" Added around here
"Arena Battle Theme No. 1" to "Arena Battle Theme 1"
"Partner's Win Theme" to "Partner Win Theme" //match the previous ones
"Partner's Loss Theme" to "Partner Loss Theme" //match the previous ones
"Arena Battle Theme No. 2" to "Arena Battle Theme 2"
"Arena Battle Theme No. 3" to "Arena Battle Theme 3"
"Normal Battle Theme" to "Hard Battle Theme"
"Normal Battle  Theme No.2" to "Normal Battle Theme"
 


//Text changes as Hex

14D717E8 to 14D71917
Changed:
4E 61 74 69 76 65 20 46 6F 72 65 73 74 20 54 68 65 6D 65 00
00 00 00 00 00 00 00 05 00 00 00 00 4E 61 74 69 76 65 20 46
6F 72 65 73 74 20 4E 69 67 68 74 20 54 68 65 6D 65 00 00 00
00 00 00 00 00 05 00 00 54 72 6F 70 69 63 61 6C 20 54 68 65
6D 65 00 00 54 72 6F 70 69 63 61 6C 20 4E 69 67 68 74 20 54
68 65 6D 65 00 00 00 00 4D 74 2E 20 50 61 6E 6F 72 61 6D 61
20 54 68 65 6D 65 00 00 4D 74 2E 20 50 61 6E 6F 72 61 6D 61
20 4E 69 67 68 74 20 54 68 65 6D 65 00 00 00 00 44 72 69 6C
6C 20 54 75 6E 6E 65 6C 20 54 68 65 6D 65 00 00 4F 67 72 65
20 46 6F 72 74 72 65 73 73 20 54 68 65 6D 65 00 4F 76 65 72
64 65 6C 6C 20 43 65 6D 65 74 65 72 79 20 54 68 65 6D 65 00
43 61 6E 79 6F 6E 20 54 68 65 6D 65 00 00 00 00 4F 67 72 65
6D 6F 6E 20 54 68 65 6D 65 20 32 00 00 00 02 00 4D 6F 6E 6F
63 72 6F 6D 6F 6E 20 53 68 6F 70 20 54 68 65 6D 65 00 00 00
4F 67 72 65 6D 6F 6E 20 54 68 65 6D 65 20 33 00 00 00 00 00 
4C 61 76 61

14D71A49 to 14D71F1C
Changed:
43 61 76 65 20 54 68 65 6D 65 00 47 72 65 79 20 4C 6F 72 64 
20 4D 61 6E 73 69 6F 6E 20 54 68 65 6D 65 00 00 00 00 00 00 
05 00 00 55 6E 64 65 72 67 72 6F 75 6E 64 20 4C 61 62 20 54 
68 65 6D 65 00 00 00 47 65 61 72 20 53 61 76 61 6E 6E 61 20 
54 68 65 6D 65 00 00 47 65 61 72 20 53 61 76 61 6E 6E 61 20 
4E 69 67 68 74 20 54 68 65 6D 65 00 00 00 00 4C 65 6F 6D 6F 
6E 20 54 68 65 6D 65 00 00 00 00 41 6D 69 64 61 20 46 6F 72 
65 73 74 20 54 68 65 6D 65 00 00 41 6D 69 64 61 20 46 6F 72 
65 73 74 20 4E 69 67 68 74 20 54 68 65 6D 65 00 00 00 00 44 
69 6E 6F 20 53 70 65 65 64 20 54 68 65 6D 65 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 05 00 00 00 44 69 6E 6F 20 53 70 65 65 
64 20 4E 69 67 68 74 20 54 68 65 6D 65 00 00 00 00 00 00 05 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 05 00 44 69 6E 6F 20 47 6C 61 63 69 61 6C 20 
54 68 65 6D 65 00 00 00 00 00 00 05 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 05 00 00 44 
69 6E 6F 20 47 6C 61 63 69 61 6C 20 4E 69 67 68 74 20 54 68 
65 6D 65 00 00 00 00 00 00 05 00 00 00 00 00 00 00 00 00 00 
00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 46 
72 65 65 7A 65 6C 61 6E 64 20 54 68 65 6D 65 00 00 00 00 46 
72 65 65 7A 65 6C 61 6E 64 20 4E 69 67 68 74 20 54 68 65 6D 
65 00 00 49 67 6C 6F 6F 20 54 68 65 6D 65 00 43 75 72 6C 69 
6E 67 20 54 68 65 6D 65 00 00 00 53 61 6E 63 74 75 61 72 79 
20 54 68 65 6D 65 00 53 61 6E 63 74 75 61 72 79 20 53 65 63 
72 65 74 20 54 68 65 6D 65 00 00 47 65 63 6B 6F 20 53 77 61 
6D 70 20 54 68 65 6D 65 00 00 00 47 65 63 6B 6F 20 53 77 61 
6D 70 20 4E 69 67 68 74 20 54 68 65 6D 65 00 4D 69 73 74 79 
20 54 72 65 65 73 20 54 68 65 6D 65 00 00 00 4D 69 73 74 79 
20 54 72 65 65 73 20 4E 69 67 68 74 20 54 68 65 6D 65 00 57 
61 72 75 4D 6F 6E 7A 61 65 6D 6F 6E 20 54 68 65 6D 65 00 54 
6F 79 20 54 6F 77 6E 20 54 68 65 6D 65 00 00 46 61 63 74 6F 
72 69 61 6C 20 54 6F 77 6E 20 44 61 79 00 00 46 61 63 74 6F 
72 69 61 6C 20 54 6F 77 6E 20 4E 69 67 68 74 20 00 00 00 53 
65 77 65 72 20 54 68 65 6D 65 00 54 72 61 73 68 20 4D 6F 75 
6E 74 61 69 6E 20 54 68 65 6D 65 00 00 00 00 54 72 61 73 68 
20 4D 6F 75 6E 74 61 69 6E 20 4E 69 67 68 74 20 54 68 65 6D 
65 00 00 42 65 74 74 6C 65 20 4C 61 6E 64 20 44 61 79 00 42 
65 74 74 6C 65 20 4C 61 6E 64 20 4E 69 67 68 74 00 00 00 00 
00 00 00 53 65 63 72 65 74 20 42 65 61 63 68 20 43 61 76 65 
20 54 68 65 6D 65 00 4C 61 73 74 20 52 6F 6F 6D 20 54 68 65 
6D 65 00 46 69 6C 65 20 43 69 74 79 20 54 68 65 6D 65 00 46 
69 6C 65 20 43 69 74 79 20 4E 69 67 68 74 20 54 68 65 6D 65 
00 00 00 54 6F 75 72 6E 61 6D 65 6E 74 20 53 74 61 72 74 20 
54 68 65 6D 65 00 00 54 6F 75 72 6E 61 6D 65 6E 74 20 50 72 
6F 67 72 65 73 73 20 54 68 65 6D 65 00 0D 00 54 6F 75 72 6E 
61 6D 65 6E 74 20 43 68 61 6D 70 69 6F 6E 20 54 68 65 6D 65 
00 00 05 00 00 00 00 50 61 72 74 6E 65 72 20 45 6E 74 72 61 
6E 63 65 20 54 68 65 6D 65 00 00 00 00 00 00 4F 70 70 6F 6E 
65 6E 74 20 45 6E 74 72 61 6E 63 65 20 54 68 65 6D 65 00 4D 
74 2E 20 49 6E 66 69 6E 69 74 79 20 54 68 65 6D 65 00 00 00 
00 00 00 41 72 65 6E 61 20 42 61 74 74 6C 65 20 54 68 65 6D 
65 20 31 00 00 00 01 00 00 00 00 50 61 72 74 6E 65 72 20 57 
69 6E 20 54 68 65 6D 65 00 00 00 50 61 72 74 6E 65 72 20 4C 
6F 73 73 20 54 68 65 6D 65 00 00 00 00 00 00 41 72 65 6E 61 
20 42 61 74 74 6C 65 20 54 68 65 6D 65 20 32 00 00 00 02 00 
00 00 00 41 72 65 6E 61 20 42 61 74 74 6C 65 20 54 68 65 6D 
65 20 33 00 00 00 03 00 00 00 00 45 76 65 6E 74 20 42 61 74 
74 6C 65 20 54 68 65 6D 65 00 00 48 61 72 64 20 42 61 74 74 
6C 65 20 54 68 65 6D 65 00 05 00 4E 6F 72 6D 61 6C 20 42 61 
74 74 6C 65 20 54 68 65 6D 65 20 20 00 00 00 00 00 00 00 4C 
61 73 74 20 42 61 74 74 6C 65 20 54 68 65 6D 65
