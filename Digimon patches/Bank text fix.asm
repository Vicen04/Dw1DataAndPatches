//This is a quick fix for the bank not showing the items you own after scrolling for a bit
//The cause of that glitch is just the game cleaning too much of the screen, which affects even the textbox that shows your inventory. This patch just lower the amount it cleans.

//I would like to show more of the function, but I have not fully read it, the change is also pretty small, so only the disassembly is shown this time


Inside the Function 80100948, used to render textboxes from what I understood

Disassembly:

         Offset      Hex               Commands

Original:

        80100bc8 64 00 02 24     li         v0,0x64   

Changed:
 
        80100bc8 1c 00 02 24     li         v0,0x1c


