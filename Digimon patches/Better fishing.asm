GetAttractedFish()
{
//code ignored
 if (fish == 6) // Seadramon
   {
     if (CurrentFrame % 8 == 0)
         ChanceofHook = 50;
//code ignored
   }
//code ignored
}


Original
80075d40 14 00 11 24     _li        s1,0x14

Changed
80075d40 32 00 11 24     _li        s1,0x32



SetUpTensionGauge()
{
//code ignored
  TensionGaugeValue = 20000;
  if (IsOldRod) 
    TensionGaugeValue = 10000;

//code ignored
}

Original:
        80072c54 10 27 02 24     _li        v0,0x2710
        80072c58 01 00 00 10     b          0x80072c60
        80072c5c 88 13 02 24     _li        v0,0x1388

Changed:
        80072c54 20 4e 02 24     _li        v0,0x4e20
        80072c58 01 00 00 10     b          0x80072c60
        80072c5c 10 27 02 24     _li        v0,0x2710


FishingLogic()
{
//code ignored
 rodMaxLenght = 400;
 if (IsOldRod)
    rodMaxLenght = 270;
//code ignored

}


Original:

        800783a0 c8 00 02 24     _li        v0,0xc8

Changed:
        800783a0 0e 01 02 24     li         v0,0x10e



Bait is located at 14BA9F54, Syd spreadsheet has all the orignal data available, my spreadsheet has all the changes.

The fish data is located at 14BAA6C0.

The items that you can fish are located at 14BA9D5C, it has this structure:

*Two bytes for each item:
  - 1 byte for the item ID.
  - 1 byte for the chance, this chance is compared with a random number between 0 and 98 (both included), if the number is lower than this value, then the item is choosen, if it is bigger, check the next item.