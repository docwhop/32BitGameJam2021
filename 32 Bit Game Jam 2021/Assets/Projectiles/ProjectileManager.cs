using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
	public static ProjectileManager Instance;

    [SerializeField] private Projectile needleProjPrefab, honeyLauncherProjPrefab, pollenatorProjPrefab, cannonProjPrefab;
	[SerializeField] private int size;

	Projectile[] needlePool, honeyLauncherPool, pollenatorPool, cannonPool;
         
	void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}

        needlePool = new Projectile[size];
        honeyLauncherPool = new Projectile[size];
        pollenatorPool = new Projectile[size];
		cannonPool = new Projectile[size];

        for (int i = 0; i < needlePool.Length; i++)
		{
            needlePool[i] = Instantiate(needleProjPrefab.gameObject, transform).GetComponent<Projectile>();
            needlePool[i].gameObject.SetActive(false);

            honeyLauncherPool[i] = Instantiate(honeyLauncherProjPrefab.gameObject, transform).GetComponent<Projectile>();
            honeyLauncherPool[i].gameObject.SetActive(false);

            pollenatorPool[i] = Instantiate(pollenatorProjPrefab.gameObject, transform).GetComponent<Projectile>();
            pollenatorPool[i].gameObject.SetActive(false);

			cannonPool[i] = Instantiate(cannonProjPrefab.gameObject, transform).GetComponent<Projectile>();
			cannonPool[i].gameObject.SetActive(false);
		}
    }

	public void SpawnProjectile(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage, WeaponName _name, Collider _ignore = null)
	{
        switch (_name)
        {
            case WeaponName.NeedleGun:
                for (int i = 0; i < needlePool.Length; i++)
                {
                    if (needlePool[i].gameObject.activeSelf == false)
                    {
						needlePool[i].gameObject.SetActive(true);

						needlePool[i].Initialize(_position, _direction, _speed, _range, _damage);

						if (_ignore != null)
						{
							needlePool[i].IgnoreCollider(_ignore);
						}
						break;
                    }
                }
                break;
            case WeaponName.HoneyLauncher:
                for (int i = 0; i < honeyLauncherPool.Length; i++)
                {
                    if (honeyLauncherPool[i].gameObject.activeSelf == false)
                    {
						honeyLauncherPool[i].gameObject.SetActive(true);

						honeyLauncherPool[i].Initialize(_position, _direction, _speed, _range, _damage);

						if (_ignore != null)
						{
							honeyLauncherPool[i].IgnoreCollider(_ignore);
						}
						break;
                    }
                }
                break;
            case WeaponName.Pollenator:
                for (int i = 0; i < pollenatorPool.Length; i++)
                {
                    if (pollenatorPool[i].gameObject.activeSelf == false)
                    {
						pollenatorPool[i].gameObject.SetActive(true);

						pollenatorPool[i].Initialize(_position, _direction, _speed, _range, _damage);

						if (_ignore != null)
						{
							pollenatorPool[i].IgnoreCollider(_ignore);
						}
						break;
                    }
                }
                break;
			case WeaponName.Cannon:
				for (int i = 0; i < cannonPool.Length; i++)
				{
					if (cannonPool[i].gameObject.activeSelf == false)
					{
						cannonPool[i].gameObject.SetActive(true);

						cannonPool[i].Initialize(_position, _direction, _speed, _range, _damage);

						if (_ignore != null)
						{
							cannonPool[i].IgnoreCollider(_ignore);
						}
						break;
					}
				}
				break;
            default:
                break;
        }

	      
	}
}
