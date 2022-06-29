using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UndertaleModLib;


//a function that takes in a string[] as object names, and returns their undertaleGameObject[]
UndertaleGameObject[] GetObjectsFromNames(string[] names)
{
  UndertaleGameObject[] arr = new UndertaleGameObject[names.Length];
  int index = 0;
  //get object for every name
  foreach (var o in Data.GameObjects)
  {
    foreach (var n in names)
    {
      if (o.Name.Content == n)
      {
        arr[index] = o;
        index++;
      }
    }
  }
  return arr;
}

//a function that takes in a pool of objects to detect and a pool of objects to randomise them to
//also takes in a uint[] to skip certain objects based on their instanceID that would cause errors if they were changed
void Randomise(UndertaleGameObject[] canBeRandomised, UndertaleGameObject[] canBeResult, uint[] ignore)
{

  Random rnd = new Random();
  foreach (var room in Data.Rooms)
  {
    foreach (var layer in room.Layers)
    {
      if (layer.InstancesData is null) continue;

      foreach (var currentObject in layer.InstancesData.Instances)
      {
        bool shouldIgnore = false;
        foreach (var id in ignore)
        {
          if (currentObject.InstanceID == id) shouldIgnore = true;
        }
        if (shouldIgnore) continue;
        int choice = rnd.Next(0, canBeResult.Length);
        foreach (var lookingFor in canBeRandomised)
        {

          if (currentObject.ObjectDefinition.Name.Content == lookingFor.Name.Content)
          {
            currentObject.ObjectDefinition = canBeResult[choice];
            break;
          }
        }
      }
    }
  }
}

//---------------------------------------------------
//actual randomising

bool randomiseEnemies = true;
bool randomiseItems = true;


//originalEnemyPool = every enemy in the game
//newEnemyPool = every enemy in the game
//ignore: gunner that starts run, "who would win" ricky in hotel-4, card players in mansion-3 (2), actors in dragon-1 cutscene (3)
string[] originalEnemyPool = new string[] { "obj_enemy_grunt", "obj_enemy_meele", "obj_enemy", "obj_enemy_shotgun", "obj_enemy_shieldcop", "obj_enemy_cop", "obj_enemy_redpomp", "obj_enemy_machinegun"};
string[] newEnemyPool = new string[] { "obj_enemy_grunt", "obj_enemy_meele", "obj_enemy", "obj_enemy_shotgun", "obj_enemy_shieldcop", "obj_enemy_cop", "obj_enemy_redpomp", "obj_enemy_machinegun" };
UndertaleGameObject[] enemiesIn = GetObjectsFromNames(originalEnemyPool);
UndertaleGameObject[] enemiesOut = GetObjectsFromNames(newEnemyPool);
if (randomiseEnemies) Randomise(enemiesIn, enemiesOut, new uint[] { 100097, 100782, 105194, 105195, 106829, 106825, 106826 });

//originalThrowablePool = every item in the game
//newThrowablePool = every item in the game
//ignore: smoke vial in quiet-1, molotov in mansion-3
string[] originalThrowablePool = new string[] { "obj_throwable_bust", "obj_throwable_lamp", "obj_throwable_butcher", "obj_throwable_plant", "obj_throwable_dec_sword", "obj_throwable_knife", "obj_throwable_pistol", "obj_throwable_beerbottle_1", "obj_throwable_beerbottle_2", "obj_throwable_beerbottle_3", "obj_throwable_beerbottle_4", "obj_throwable_explosive_vial", "obj_throwable_smoke_vial", "obj_throwable_remote_mine", "obj_flamethrower"};
string[] newThrowablePool = new string[] { "obj_throwable_bust", "obj_throwable_lamp", "obj_throwable_butcher", "obj_throwable_plant", "obj_throwable_dec_sword", "obj_throwable_knife", "obj_throwable_pistol", "obj_throwable_beerbottle_1", "obj_throwable_beerbottle_2", "obj_throwable_beerbottle_3", "obj_throwable_beerbottle_4", "obj_throwable_explosive_vial", "obj_throwable_smoke_vial", "obj_throwable_remote_mine", "obj_flamethrower"};
UndertaleGameObject[] throwablesIn = GetObjectsFromNames(originalThrowablePool);
UndertaleGameObject[] throwablesOut = GetObjectsFromNames(newThrowablePool);
if (randomiseItems) Randomise(throwablesIn, throwablesOut, new uint[] { 103652, 105270 });
