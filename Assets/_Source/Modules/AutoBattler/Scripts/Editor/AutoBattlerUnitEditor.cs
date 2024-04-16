#if UNITY_EDITOR
using UnityEditor;

namespace AutoBattler
{
    [CustomEditor(typeof(AutoBattlerUnit))]
    public class AutoBattlerUnitEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            serializedObject.Update();

            AutoBattlerUnit autoBattlerUnit = (AutoBattlerUnit)target;

            if (autoBattlerUnit.IsHero && autoBattlerUnit.HasAutoAttackSkill == false)
                DrawPropertiesExcluding(serializedObject, new string[] { "_autoAttackSkill" });
            else if (autoBattlerUnit.IsHero && autoBattlerUnit.HasAutoAttackSkill)
                DrawPropertiesExcluding(serializedObject);
            else if (!autoBattlerUnit.IsHero && autoBattlerUnit.HasAutoAttackSkill)
                DrawPropertiesExcluding(serializedObject, new string[] { "_passiveHeroSkill", "_activeHeroSkill", "IsHero" });
            else
                DrawPropertiesExcluding(serializedObject, new string[] { "_autoAttackSkill", "_passiveHeroSkill", "_activeHeroSkill" });

            serializedObject.ApplyModifiedProperties();

        }
    }
}
#endif