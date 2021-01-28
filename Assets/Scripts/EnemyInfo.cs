using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyInfo", menuName = "ScripatbleObject/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private float enemyMaxHp;
    [SerializeField] private float enemyAtkDamage;
    [SerializeField] private float enemyAtkRange;
    [SerializeField] private float enemyChasingRange;
    [SerializeField] private float enemySpeed;
    [SerializeField] private int enemyExp;

    [SerializeField] private int blueGem;
    [SerializeField] private Color enemyNameTxtColor;

    public string EnemyName { get { return enemyName; } }
    public float EnemyMaxHp { get { return enemyMaxHp; } }
    public float EnemyAtkDamage { get { return enemyAtkDamage; } }
    public float EnemyAtkRange { get { return enemyAtkRange; } }
    public float EnemyChasingRange { get { return enemyChasingRange; } }
    public float EnemySpeed { get { return enemySpeed; } }
    public int EnemyExp { get { return EnemyExp; } }
    public int BlueGem { get { return blueGem; } }
    public Color EnemyNameTxtColor { get { return enemyNameTxtColor; } }
}
