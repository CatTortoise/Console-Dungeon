using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Dungeon
{
    static class Action
    {
        public static readonly int numberOfActions = 5;
        public enum Actions
        {
            DealDamage,
            ShieldYourself,
            Heal,
            UpgradEquipment,
            GoIdol,
            Dead
        }
        public static Entity Target;
        public static int Heal;
        public static int IncreaseAmount;
        public static Entity.Equipment Euipment;

        static public void UpgradEquipment(Entity entity, int increaseAmount, Entity.Equipment equipment)
        {
            IncreaseAmount = increaseAmount;
            Euipment = equipment;
            entity.ActionsSelection(Actions.UpgradEquipment);
        }

        static public void DealDamage(Entity target, Entity attacker)
        {
            Target = target;
            attacker.ActionsSelection(Actions.DealDamage);
        }

        static public void UseHeal(Entity target,int heal)
        {
            Target = target;
            Heal = heal;
            target.ActionsSelection(Actions.Heal);
        }

        static public void ShieldYourself(Entity target)
        {
            Target = target;
            target.ActionsSelection(Actions.ShieldYourself);
        }
        static public void GoIdol(Entity target)
        {
            Target = target;
            target.ActionsSelection(Actions.GoIdol);
        }



    }
}
