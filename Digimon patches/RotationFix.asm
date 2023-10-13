//This is my version of the rotation fix, I'm aware that the randomizer has it's own version

//Due to an oversight, the game is unable to handle 180 degrees rotations. This would cause the game to freeze due to it being unable to proceed with the code.

Original:

int rotateEntity(rotationPtr, targetAngle, cClockwiseDiff, clockwiseDiff, rotationSpeed) 
{
//Code ignored
if(currentRotation < targetAngle) 
 {
  if(cClockwiseDiff < clockwiseDiff)
  {
     //code ignored
  }
  else if(clockwiseDiff < cClockwiseDiff) 
  {
      //code ignored
  }
 }
 else if(targetAngle < currentRotation) 
 {  
    if(cClockwiseDiff < clockwiseDiff) 
  {
    //code ignored
  }
  else if(clockwiseDiff < cClockwiseDiff)
  {
      // code ignored
  }
 }
// code ignored
}

Changed:

int rotateEntity(rotationPtr, targetAngle, cClockwiseDiff, clockwiseDiff, rotationSpeed) 
{
//Code ignored
if(currentRotation < targetAngle) 
 {
  if(cClockwiseDiff < clockwiseDiff)
  {
     //code ignored
  }
  else
  {
      //code ignored
  }
 }
 else if(targetAngle < currentRotation) 
 {  
  if(cClockwiseDiff < clockwiseDiff) 
  {
    //code ignored
  }
  else
  {
      // code ignored
  }
 }
// code ignored
}


Disassembly:

        Offset       Hex         Commands

Original:

        800b7008 2a 08 e8 00     slt        at,a3,t0
        800b700c 2b 00 20 10     beq        at,zero,0x800b70bc

        800b707c 2a 08 e8 00     slt        at,a3,t0
        800b7080 0e 00 20 10     beq        at,zero,0x800b70bc

Changed:

        800b7008 00 00 00 00     nop        
        800b700c 00 00 00 00     nop   

        800b707c 00 00 00 00     nop   
        800b7080 00 00 00 00     nop   

