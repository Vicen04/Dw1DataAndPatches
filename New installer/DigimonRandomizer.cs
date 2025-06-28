using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

public class digimonNecessaryData
{
    public List<byte> Attacks { get; set; }
    public byte Level { get; set; }
    public bool HasFinisher { get; set; }
    public byte Type1 { get; set; }
    public int dataWeight { get; set; }

  public digimonNecessaryData(uint currentOffset, ref Stream bin)
  {
      Attacks = new List<byte>();
      HasFinisher = false;

      bin.Position = currentOffset + 0x1C;
      Level = (byte)bin.ReadByte();

      bin.Position = currentOffset + 0x1E;
      Type1 = (byte)bin.ReadByte();

      bin.Position = currentOffset + 0x23;
      for (int j = 0; j < 16; j++)
      {
        byte attackID = (byte)bin.ReadByte();
        if (attackID == 0xFF)
          break;
        else if (attackID > 56)
          HasFinisher = true;
        Attacks.Add(attackID);
      }
    }
}

public class TechNecessaryData
{
  public short Power { get; set; }
  public byte Type { get; set; }

  public TechNecessaryData(uint currentOffset, BinaryReader reader, ref Stream bin)
  {
      bin.Position = currentOffset;
      Power = reader.ReadInt16();
      bin.Position = currentOffset + 5;
      Type = (byte)bin.ReadByte();
      
  }
}

public class digimonMapData
{
    public short[] Attacks { get; set; }
    public short[] AttackChances { get; set; }
    public short Money { get; set; }
    public short DigimonID { get; set; }
    public short HPMax { get; set; }
    public short HPCurrent { get; set; }
    public short MPMax { get; set; }
    public short MPCurrent { get; set; }
    public short Offense { get; set; }
    public short Defense { get; set; }
    public short Speed { get; set; }
    public short Brains { get; set; }
}

public class MapData
{
    public List<digimonMapData> digimonsMapData { get; set; }
}

public class DigimonRandomizer
{

  bool isHardcore, tHardcore;
  Stream bin;

  List<digimonNecessaryData> digimonData;
  List<TechNecessaryData> techData;

  List<MapData> NPCMapData, bossMapData;

  public DigimonRandomizer(bool hardcore, bool trueHardcore, ref Stream binOG, BinaryReader reader)
  {
    isHardcore = hardcore;
    tHardcore = trueHardcore;
    bin = binOG;

    uint dataOffset = 0x14D6E9DC, currentOffset = dataOffset;
    uint[] jumpOffsets = { 0x14D6EB28, 0x14D6F458, 0x14D6FD88, 0x14D706B8, 0x14D70FE8, 0x79999999 };
    int jumpValue = 0;

    digimonData = new List<digimonNecessaryData>();

    for (int i = 0; i < 176; i++)
    {
      currentOffset = currentOffset + 0x34;
      if (currentOffset > jumpOffsets[jumpValue])
      {
        currentOffset = currentOffset + 0x130;
        jumpValue++;
      }

      digimonData.Add(new digimonNecessaryData(currentOffset, ref bin));


    }

    techData = new List<TechNecessaryData>();

    dataOffset = 0x14D66DF8;
    currentOffset = dataOffset;

    for (int i = 0; i < 57; i++)
    {      
      techData.Add(new TechNecessaryData(currentOffset, reader, ref bin));
      currentOffset = currentOffset + 0x10;
    }

    NPCMapData = new List<MapData>();
    bossMapData = new List<MapData>();
  }


  public void RandomizeDigimonNPC(int option, bool bosses)
  {



  }

  public void RandomizeNPCStats(int option, bool bosses)
  {
    foreach (MapData maps in NPCMapData)
    {
      foreach (digimonMapData data in maps.digimonsMapData)
      {

      }
    }


    if (bosses)
    {
      foreach (MapData maps in bossMapData)
      {
        foreach (digimonMapData data in maps.digimonsMapData)
        {

        }
      }
    }

  }

  public void RandomizeNPCTechs(bool bosses, bool ignoreNPC)
  {
    if (!bosses && ignoreNPC)
      return;

    if (!ignoreNPC)
    {
      foreach (MapData maps in NPCMapData)
      {
        foreach (digimonMapData data in maps.digimonsMapData)
        {

        }
      }
    }
    if (bosses)
    {
      foreach (MapData maps in bossMapData)
      {
        foreach (digimonMapData data in maps.digimonsMapData)
        {

        }
      }
    }
  }

  public void RandomizeNPCMoney(int option, bool bosses)
  {
    foreach (MapData maps in NPCMapData)
    {
      foreach (digimonMapData data in maps.digimonsMapData)
      {

      }
    }
    if (bosses)
    {
      foreach (MapData maps in bossMapData)
      {
        foreach (digimonMapData data in maps.digimonsMapData)
        {

        }
      }
    }
  }

  public void RandomizeStarter(int option, Random numberGenerator)
  {
    List<byte> possibleDigimon = new List<byte>();

    for (int i = 1; i < 66; i++)
    {
      if (option < 5)
      {
        if (digimonData[i].Level == (option + 1))
          possibleDigimon.Add((byte)i);
      }
      else
        possibleDigimon.Add((byte)i);
    }

    uint[] digimonOffsets = { 0x14D271B8, 0x14D271C0, 0x14D19DB0, 0x14D19DD4 },
    techOffset = { 0x14CD1D50, 0x14CD1D30, 0x14D19D98, 0x14D19DC0 },
    learnOffset = { 0x14CD1D60, 0x14CD1D40, 0x14D19DA8, 0x14D19DD0 },
    checkOffset = { 0x14CD1D18, 0x14CD1D44 };

    for (int i = 0; i < 4; i++)
    {
      byte selectedDigimon = possibleDigimon[numberGenerator.Next(possibleDigimon.Count)];
      if (i < 2)
      {
        bin.Position = checkOffset[i];
        bin.WriteByte(selectedDigimon);
      }
      bin.Position = digimonOffsets[i];
      bin.WriteByte(selectedDigimon);
      int count = 0;
      foreach (byte attack in digimonData[selectedDigimon].Attacks)
      {
        if (attack > 56)
          continue;
        if (techData[attack].Type == digimonData[selectedDigimon].Type1)
        {
          bin.Position = techOffset[i];
          bin.WriteByte((byte)(count + 0x2E));
          bin.Position = learnOffset[i];
          bin.WriteByte(attack);
          break;
        }
        count++;
      }
      if (count == digimonData[selectedDigimon].Attacks.Count)
      {
        bin.Position = learnOffset[i];
        bin.WriteByte(digimonData[selectedDigimon].Attacks[0]);
        bin.Position = techOffset[i];
        bin.WriteByte(0x2E);
      }

      possibleDigimon.Remove(selectedDigimon);
    }

  }

  public void StarterTech(int option, Random numberGenerator)
  {
    uint[] digimonOffsets = { 0x14D271B8, 0x14D271C0, 0x14D19DB0, 0x14D19DD4 },
    techOffset = { 0x14CD1D50, 0x14CD1D30, 0x14D19D98, 0x14D19DC0 },
    learnOffset = { 0x14CD1D60, 0x14CD1D40, 0x14D19DA8, 0x14D19DD0 };
    for (int i = 0; i < 4; i++)
    {
      bin.Position = digimonOffsets[i];
      byte Digimon = (byte)bin.ReadByte();

      int count = 0;

      switch (option)
      {
        case 0:
          short currentPower = 1000;
          int selectedAttack = 0;

          foreach (byte attack in digimonData[Digimon].Attacks)
          {
            if (attack < 57 && techData[attack].Power < currentPower)
            {
              selectedAttack = count;
              currentPower = techData[attack].Power;
            }
            count++;
          }
          bin.Position = learnOffset[i];
          bin.WriteByte(digimonData[Digimon].Attacks[selectedAttack]);
          bin.Position = techOffset[i];
          bin.WriteByte((byte)(0x2E + selectedAttack));
          break;
        case 1:
          byte randomTech = (byte)numberGenerator.Next(digimonData[Digimon].Attacks.Count);
          if (digimonData[Digimon].Attacks[randomTech] > 56)
            randomTech--;
          bin.Position = learnOffset[i];
          bin.WriteByte(digimonData[Digimon].Attacks[randomTech]);
          bin.Position = techOffset[i];
          bin.WriteByte((byte)(0x2E + randomTech));
          break;
      }



    }
  }

  public void StarterStats(int option, BinaryWriter writter, BinaryReader reader, Random numberGenerator)
  {
    uint offsetStart1 = 0x1407E2CE, offsetStart2 = 0x1407E2F2;
    bin.Position = offsetStart1;
    switch (option)
    {
      case 0:
        if (isHardcore)
        {
          if (tHardcore)
          {
            SetStarterStats(writter, reader, numberGenerator, 91, 40);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 91, 40);
          }
          else
          {
            SetStarterStats(writter, reader, numberGenerator, 56, 55);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 56, 55);
          }
        }
        else
        {
          SetStarterStats(writter, reader, numberGenerator, 51, 50);
          bin.Position = offsetStart2;
          SetStarterStats(writter, reader, numberGenerator, 51, 50);
        }
        break;
      case 1:
        if (isHardcore)
        {
          if (tHardcore)
          {
            SetStarterStats(writter, reader, numberGenerator, 180, 1);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 180, 1);
          }
          else
          {
            SetStarterStats(writter, reader, numberGenerator, 146, 5);
            bin.Position = offsetStart2;
            SetStarterStats(writter, reader, numberGenerator, 146, 5);
          }
        }
        else
        {
          SetStarterStats(writter, reader, numberGenerator, 121, 10);
          bin.Position = offsetStart2;
          SetStarterStats(writter, reader, numberGenerator, 121, 10);
        }

        break;
    }

  }

  public void TournamentNPC(int option, BinaryReader reader, BinaryWriter writer, Random numberGenerator)
  {
    uint[] tournamentPtr = { 0x140A49FC, 0x140A4AFA, 0x140A4BFC, 0x140A4CFE, 0x140A4E00, 0x140A502E, 0x140A529A, 0x140A53D6, 0x140A551E, 0x140A5660, 0x140A57C6,
                                  0x140A5900, 0x140A5A4A, 0x140A5CC8, 0x140A5DFA, 0x140A5F46, 0x140A6080, 0x140A61B8, 0x140A62E8, 0x140A652A, 0x140A665E };

    List<short> jumps = new List<short>();

    foreach (uint ptr in tournamentPtr)
    {
      bin.Position = ptr;
      jumps.Add(reader.ReadInt16());
    }

    numberGenerator.Shuffle(tournamentPtr);

    for (int i = 0; i < tournamentPtr.Length; i++)
    {
      bin.Position = tournamentPtr[i];
      writer.Write(jumps[i]);
    }

    uint bugOffset = 0x1402897E;
    bin.Position = bugOffset;
    switch (option)
    {
      case 0:
        writer.Write((short)11151);
        break;
      case 1:
        writer.Write((short)11217);
        uint[] digimonValues = { 0x140A69A2, 0x140A69B8, 0x140A69FB, 0x140A6A4C, 0x140A6A7A, 0x140A6A92, 0x140A6A96, 0x140A6AA6, 0x140A6AB6, 0x140A6AC6, 0x140A6AD6,
                                 0x140A6B10, 0x140A6B26, 0x140A6B4A, 0x140A6B63, 0x140A6B81, 0x140A6B94, 0x140A6BA3, 0x140A6BAC, 0x140A6BC6, 0x140A6BE1, 0x140A6BF7, 0x140A6C11 };

        List<byte> currentTournament = new List<byte>();
        for (int i = 1; i < 114; i++)
        {
          if (i == 62)
            currentTournament.Add(115);
          else
            currentTournament.Add((byte)i);
        }
        byte[] copyDigimon = currentTournament.ToArray();
        for (int i = 0; i < digimonValues.Length; i++)
        {
          int count = 0;
          while (true)
          {
            bin.Position = digimonValues[i] + count;
            if (bin.ReadByte() > 253)
              break;
            bin.Position = digimonValues[i] + count;
            byte random = currentTournament[numberGenerator.Next(currentTournament.Count)];
            bin.WriteByte(random);
            count++;
            currentTournament.Remove(random);
          }
          currentTournament.Clear();
          currentTournament.AddRange(copyDigimon);
        }
        break;
    }
  }

  public void Recruitments(int option, Random numberGenerator, BinaryWriter writter)
  {
    List<uint[]> recruitTriggers =
    [
        [ 0x13FD6576, 0x13FE909A, 0x1409FC94, 0x140B76F6], //Betamon
        [0x1407870C],//Devimon
        [0x14078708],//Airdramon
        [0x14016CB6, 0x140172B6, 0x1409FFD6, 0x140B8570, 0x140B860E, 0x140B860E],//Tyrannomon
        [0x13FD696A, 0x13FD6998, 0x13FF1A6C, 0x13FF44D8, 0x13FF5740, 0x13FF5886, 0x140A009A, 0x140B7AEA, 0x140B7B18],//Meramon
        [0x13FE1D48, 0x13FE19B2, 0x14059D08, 0x140A104A, 0x14D725F0],//Seadramon
        [0x13FD9780, 0x13FD9794, 0x14073C7C, 0x140503F8, 0x14050CAC, 0x14074630, 0x140A0F72, 0x140BA900, 0x140BA914],//Numemon
        [0x14078704],//MetalGreymon
        [0x13FD6792, 0x13FED052, 0x140A0E94, 0x140B7912],//Mamemon
        [0x13FD8E96, 0x14046696, 0x14061FA8, 0x140BA016],//Monzaemon
        [0x13FD7C5A, 0x140360A2 ,0x140A0B80, 0x140B8DDA],//Gabumon
        [0x13FD7106, 0x1400DD0A, 0x140A045E, 0x140B8286],//Elecmon
        [0x13FD797E, 0x14029508, 0x140B8AFE],//Kabuterimon
        [0x140209F6, 0x14020E84],//Angemon
        [0x13FD767C, 0x13FD769C ,0x1401F636, 0x140A0236, 0x140B87FC, 0x140B881C],//Garurumon
        [0x13FD7EE2, 0x14040D64, 0x140A0152, 0x140B9062],//Frigimon
        [0x13FD6DE0, 0x1400BA8C, 0x1409A90A, 0x140B7F60],//SkullGreymon
        [0x13FD8322, 0x13FD9982, 0x1404CF3E, 0x140A1824, 0x140B94A2, 0x140BAB02],//MetalMamemon
        [0x1407BDD8, 0x1407BE1E, 0x1407C320, 0x140A1908],//Vademon
        [0x13FD715E, 0x1400E6D6, 0x140A072E, 0x140B82DE],//Patamon
        [0x13FD627A, 0x13FD6294, 0x13FD62B2, 0x13FDDEA8, 0x13FDE354, 0x140A22A6, 0x140B73FA, 0x140B7414, 0x140B7432],//Kunemon
        [0x13FD9922, 0x1407B436, 0x1407B9AE, 0x140A0654, 0x140BAAA2],//Unimon
        [0x13FD689E, 0x13FF1972, 0x13FF0F98, 0x140B7A1E],//Ogremon
        [0x13FF8018, 0x13FD6A34, 0x1409FD2E, 0x140B7BB4, 0x14D725CC],//Bakemon
        [0x13FD69B8, 0x13FF6080, 0x13FF6092, 0x13FF6456, 0x140A0A9C, 0x140B7B38],//Drimogemon
        [0x14057792, 0x1405779C],//Sukamon
        [0x1404F994, 0x1404FAD8],//Andromon
        [0x13FD84B8, 0x14052A7A, 0x140A1240, 0x140B9638],//Giromon
        [0x13FD630C, 0x13FD6348, 0x13FD6362, 0x13FDF1CC, 0x13FDF210, 0x13FDF54E, 0x140A1776, 0x140B748C, 0x140B74C8, 0x140B74E2],//Etemon
        [0x13FD71AC, 0x1400EFFC, 0x1400F148, 0x1400F292, 0x1400F50C, 0x1400F914, 0x140A0732, 0x140B832C],//Biyomon           
        [0x13FD6D72, 0x14000C52, 0x140A05B0, 0x140B7EF2],//Monochromon
        [0x13FD72F4, 0x14012928, 0x140122D6, 0x140B8474],//Leomon
        [0x13FE08F2, 0x13FE0398, 0x13FE03EE, 0x13FE056A, 0x13FE0908, 0x1409FBF6],//Coelamon
        [0x13FD7B84, 0x14032EE4, 0x14032EF4, 0x14032FA4, 0x14059D20, 0x14059F00, 0x140A050E, 0x140B8D04],//Kokatorimon
        [0x13FD79A8, 0x1402A742, 0x140B8B28],//Kuwagamon
        [0x1403CD1C, 0x1403CE84, 0x1403D04A, 0x1403D1B2, 0x1403DF1C, 0x1403E084, 0x140A0DD4],//Mojyamon
        [0x13FFFADC, 0x14019D7A, 0x140304F2, 0x1404BD10, 0x14074852],//Nanimon       
        [0x13FD9B62, 0x14081AA4, 0x14081CC4, 0x140BACE2],//Megadramon
        [0x13FD649E, 0x13FE6B52, 0x140A16B6, 0x140B761E],//Piximon
        [0x13FD9860, 0x1407737E, 0x1407750A, 0x140A1DFE, 0x140BA9E0],//Digitamamon
        [0x1403F860, 0x13FD7E36, 0x14095A7E, 0x14095DD2, 0x14096844, 0x140A09CC, 0x140B8FB6],//Penguinmon
        [0x13FD63CE, 0x13FD63E2, 0x13FE4494, 0x13FE4B16, 0x140A0C5A, 0x140B754E, 0x140B7562],//Ninjamon
        [0x14010A84],//Phoenixmon Arena
        [0x14010D6C],//H-Kabuterimon Arena
    ];

    List<short> triggers = new List<short>() {204, 206, 207, 208, 209, 210, 211, 212, 213, 214, 217, 218, 219, 220, 222, 223, 226, 227, 228, 231, 232, 233, 234, 237, 238, 239,
                                              240, 241, 242, 245, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260};

    uint[] prosperityOffsets = { 0x13FDE359, 0x13FDEB91, 0x13FDF553, 0x13FE08F7, 0x13FE1D4D, 0x13FE4B1B, 0x13FE6B57, 0x13FE88E5, 0x13FE909F, 0x13FEB641, 0x13FED04F, 0x13FF1977,
                                 0x13FF5733, 0x13FF645B, 0x13FF801D, 0x13FFFAE1, 0x14000C4F, 0x1400BA91, 0x1400DD0F, 0x1400E6DB, 0x1400F919, 0x1401292D, 0x140172BB, 0x1401F63B,
                                 0x14020E89, 0x1402950D, 0x1402A747, 0x1402BAA7, 0x140304F7, 0x140360A7, 0x140396B5, 0x1403AAFB, 0x1403CE8D, 0x14040D69, 0x140450B3, 0x1404776D,
                                 0x1404CF43, 0x1404FADD, 0x14052A7F, 0x140577A1, 0x1405B8CD, 0x14074635, 0x1407750F, 0x14078711, 0x140788F7, 0x1407B9B3, 0x1407C325, 0x14081CC9, 0x14096849 };

    if (isHardcore)
    {
      prosperityOffsets[3] = 0x13FE0891; //Coelamon
      prosperityOffsets[9] = 0x13FEB63F; //Centarumon
      prosperityOffsets[14] = 0x13FF8017; //Bakemon

      recruitTriggers[23][0] = 0x13FF8012; //Bakemon
      recruitTriggers[33][0] = 0x13FE088C; //Coelamon
      recruitTriggers[40][0] = 0x1403F868; //Penguinmon
    }

    switch (option)
      {
        case 0:
          List<byte> currentProsperity = new List<byte>();
          foreach (uint offset in prosperityOffsets)
          {
            bin.Position = offset;
            currentProsperity.Add((byte)bin.ReadByte());
          }

          byte[] shuffledProsperity = currentProsperity.ToArray();
          numberGenerator.Shuffle(shuffledProsperity);
          SetProsperity(prosperityOffsets, shuffledProsperity, isHardcore);
          break;
        case 1:
          byte[] prosperity = { 10, 7, 5, 5, 5, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
          numberGenerator.Shuffle(prosperity);
          SetProsperity(prosperityOffsets, prosperity, isHardcore);
          triggers.AddRange([203, 205, 221, 224, 225, 235, 246, 261, 236]);
          recruitTriggers.AddRange(
            [[0x13FD79D2, 0x13FFF39C, 0x13FFF3B8, 0x1402BAA2, 0x1402BFDE, 0x140B8B52], // Agumon
            [0x1405B446, 0x1405B8C8, 0x1409E4BA, 0x1409E4D6, 0x140A092C] , // Greymon
            [0x1403A5A2, 0x1403AAF6, 0x140A0306, 0x14D725C6], // Birdramon
            [0x13FD7E6C, 0x1403E676, 0x140441D4, 0x14044250, 0x140450AE, 0x14059D10, 0x14059F08, 0x1406193E, 0x140B8FEC], // Whamon
            [0x13FE7AD8, 0x13FE7BF2, 0x13FE88E0, 0x1405ACC8, 0x1405AD10, 0x1405AF5A, 0x1409FDD0], // Vegiemon
            [0x13FD6B56, 0x13FD7D4C, 0x13FFA106, 0x13FFF3C4, 0x140396B0, 0x140A03A6, 0x140B7CD6, 0x140B8ECC], // Shellmon    
            [0x13FD62D6, 0x13FDEB8C, 0x140A21C0, 0x140B7456],//Palmon        
            [0x1404591C]]); // MegaSeadramon Arena

        if (isHardcore)
          recruitTriggers.Add([0x13FE9770, 0x13FE9FC2, 0x13FEA132, 0x13FEA3D4, 0x13FEA49A, 0x13FEA560, 0x13FEA626, 0x13FEA710, 0x13FEA92A, 0x13FEB63A, 0x13FEB986, 0x13FEBDDE,
          0x13FEAA28, 0x13FEAB12, 0x13FEAC10, 0x13FEAD0E, 0x13FEAE0C, 0x13FEAEDE, 0x13FEAFDC, 0x13FEB1DE, 0x13FEB2CC, 0x13FEB3BA, 0x13FEB4AC, 0x13FEB7F8, 0x13FEBC52, 0x1409FB24]);
        else
          recruitTriggers.Add([0x13FE9770, 0x13FE9FC2, 0x13FEA132, 0x13FEA3D4, 0x13FEA49A, 0x13FEA560, 0x13FEA626, 0x13FEA710, 0x13FEA92A, 0x13FEB63C, 0x13FEB988, 0x13FEBDE0,
          0x13FEAA28, 0x13FEAB12, 0x13FEAC10, 0x13FEAD0E, 0x13FEAE0C, 0x13FEAEDE, 0x13FEAFDC, 0x13FEB1DE, 0x13FEB2CC, 0x13FEB3BA, 0x13FEB4AC, 0x13FEB7F8, 0x13FEBC52, 0x1409FB24]);

          break;
      }

    short[] ShuffledTriggers = triggers.ToArray();
    numberGenerator.Shuffle(ShuffledTriggers);

    for (int i = 0; i < recruitTriggers.Count; i++)
    {
      for (int j = 0; j < recruitTriggers[i].Length; j++)
      {
        bin.Position = recruitTriggers[i][j];
        writter.Write(ShuffledTriggers[i]);
      }
    }

  }


  void SetStarterStats(BinaryWriter writter, BinaryReader reader, Random numberGenerator, int maxRNG, int minValue)
  {
    short HP = (short)((numberGenerator.Next(maxRNG) + minValue) * 10);
    short MP = (short)((numberGenerator.Next(maxRNG) + minValue) * 10);
    for (int i = 0; i < 8; i++)
    {
      if (i < 4)
        writter.Write((short)(numberGenerator.Next(maxRNG) + minValue));
      else if (i == 4 || i == 6)
        writter.Write(HP);
      else
        writter.Write(MP);
      reader.ReadInt16();
    }
  }

  void SetProsperity(uint[] offsets, byte[] prosperity, bool hardcore)
  {
    for (int i = 0; i < prosperity.Length; i++)
    {
      bin.Position = offsets[i];
      bin.WriteByte(prosperity[i]);
      if (i == 9) //Centarumon
      {
        if (hardcore)
        {
          bin.Position = 0x13FEB98B;
          bin.WriteByte(prosperity[i]);
          bin.Position = 0x13FEBDE5;
          bin.WriteByte(prosperity[i]);
        }
        else
        {
          bin.Position = 0x13FEB98D;
          bin.WriteByte(prosperity[i]);
          bin.Position = 0x13FEBDE7;
          bin.WriteByte(prosperity[i]);
        }
      }
      else if (i == 15)//Nanimon
      {
        bin.Position = 0x14019D7F;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x140304F7;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x1404BD15;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x14074857;
        bin.WriteByte(prosperity[i]);
      }
      else if (i == 32) //Mojyamon
      {
        bin.Position = 0x1403D1BB;
        bin.WriteByte(prosperity[i]);
        bin.Position = 0x1403E08D;
        bin.WriteByte(prosperity[i]);
      }
    }
  }
    
    /*#(if trigger list, trigger, name list, digimon).
          254, 0x36 ), #Megadramon -- does nothing and has no Jijimon message
        ( ( 0x140B600A, 0x13FD9396, 0x140BA516 ),
          ( 0x13FE6B1A, 0x13FE6F98, 0x13FE75D6 ),
          255, 0x37 ), #Piximon
        ( ( 0x13FD9210, 0x140BA390, 0x13FD906E, 0x140AEF36, 0x140B67EE, 0x140BA1EE ),
          ( 0x140774C8, 0x140788B0 ),
          256, 0x38 ), #Digitamamon
        ( ( 0x14066400, 0x140BAB1E, 0x140632A4, 0x140B53E6, 0x140663AC, 0x14063250, 0x14066374, 0x14095A7E,
            0x14063218, 0x1409761C, 0x13FD999E ),
          ( 0x14096804, ),
          257, 0x39 ), #Penguinmon
        ( ( 0x140B573A, 0x13FD95CE, 0x140BA74E ),
          ( 0x13FE4AD6, ),
          258, 0x3A )  #Ninjamon
        )*/
}
