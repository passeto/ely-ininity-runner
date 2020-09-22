using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour
{

  public Rigidbody player;
  public GameObject scenario;
  public float speedScenario;
  private int currentLane;

  public float laneDistance;
  private Vector3 target;
  private Vector2 initalPosition;

	public GameObject chao;

	public GameObject bloco;

	public GameObject moeda;

	private int estagioAtual = 1;

    // Start is called before the first frame update
    void Start()
    {
     currentLane = 1;   
     target = player.transform.position;
    }

    // Update is called once per frame
    void Update() {
   

      int newLane = -1;
      // keyboard
      if(Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2) {
        newLane = currentLane + 1;
      } else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0) {
        newLane = currentLane - 1;
      }
      
      // mouse
      if(Input.GetMouseButtonDown(0)) {
        initalPosition = Input.mousePosition;
      } else if (Input.GetMouseButtonUp(0)) {
        if (Input.mousePosition.x > initalPosition.x && currentLane < 2) {
          newLane = currentLane + 1;
        } else if (Input.mousePosition.x < initalPosition.x && currentLane > 0) {
          newLane = currentLane - 1;
        }
      }

      // Touch
      if (Input.touchCount >= 1) {
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
          initalPosition = Input.GetTouch(0).position;
        } else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) {
          if (Input.GetTouch(0).position.x > initalPosition.x && currentLane < 2) {
            newLane = currentLane + 1;    
          } else if (Input.GetTouch(0).position.x < initalPosition.x && currentLane > 0) {
            newLane = currentLane - 1;
          }
        }
      } 
      if (newLane>= 0){
        currentLane = newLane;
        target = new Vector3((currentLane - 1) * laneDistance, player.transform.position.y, player.transform.position.z);
      }
      if(player.transform.position.x != target.x){
        player.transform.position = Vector3.Lerp(player.transform.position, target, 5*Time.deltaTime);
      }

      scenario.transform.Translate(0,0, speedScenario * Time.deltaTime * -1);
    }

	float estagio =
		Mathf.Floor(((cenario.transform.position.z-50.0F) / -100.0F))+1;
	if (estagio>estagioAtual) {
		GameObject chao2 = Instantiate(chao);
		float chao2z = ((100*estagioAtual)+50) +
			cenario.transform.position.z;
		chao2.transform.SetParent(cenario.transform);
		chao2.transform.position = new
			Vector3(chao.transform.position.x,chao.transform.position.y, chao2z);
		estagioAtual++;
		montarCenario();
	}
	}
          
     void montarCenario() {
	  for (int i=2; i<10;i++) {
		int elemento1 = Random.Range(0,3);
		int elemento2 = Random.Range(0,3);
		int elemento3 = Random.Range(0,3);
		//0 = nada / 1 = bloco / 2=moeda
		if (elemento1==1 && elemento2==1 && elemento3==1) {
			elemento2 = 0;
		}
		if (elemento1==1) { instanciarBloco(i, 0);
		}else if (elemento1==2) { instanciarMoeda(i, 0);
		}
		if (elemento2==1) { instanciarBloco(i, 1);
		}else if (elemento2==2) { instanciarMoeda(i, 1);
		}
		if (elemento3==1) { instanciarBloco(i, 2);
		}else if (elemento3==2) { instanciarMoeda(i, 2);
		}
	}
}

void instanciarBloco(int posicaoz, int posicaox) {
	GameObject bloco2 = Instantiate(obstaculo);
	float posz = ((10*posicaoz)+((estagioAtual-1)*100)) +
		cenario.transform.position.z;
	float posx = (posicaox-1)*distanciaRaia;
	bloco2.transform.SetParent(cenario.transform);
	bloco2.transform.position = new Vector3(posx,0.5F, posz);
}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("obstacle"))
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
}
