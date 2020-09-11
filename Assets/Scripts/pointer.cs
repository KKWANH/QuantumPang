﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour
{
	private SpriteRenderer	spRender;
	public	string			color;
	public	int[]			location;
	public	GameObject[]	cube;
	public	Sprite[]		colorbox;
	public	AudioClip[]		sounds;
	private	AudioSource		audio;
	private int[]			checkbox;
	private	double			point;

	void					Start() {
		point = 0.0;
		color = "red";
		spRender = GetComponent<SpriteRenderer>();
		location = new int[2];
		location[0] = 0;
		location[1] = 0;
		this.audio = this.gameObject.AddComponent<AudioSource> ();
		this.audio.loop = false;
	}

    void					Update() {
		Color();
		Move();
    }

	void					Move() {
		Vector3 spotPos = transform.position;
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		// 스페이스바일 때
		if(Input.GetKeyDown(KeyCode.Space)) {
			double	cubeX, cubeY, pointX, pointY;
			for (int i=0; i<100; i++) {
				// 좌표값 계산하는 부분입니다.
				cubeX = System.Math.Truncate(cube[i].transform.position.x) + 1;
				pointX = System.Math.Truncate(spotPos.x);
				cubeY = System.Math.Truncate(cube[i].transform.position.y);
				pointY = System.Math.Truncate(spotPos.y);
				if (cubeX == pointX && cubeY == pointY) {
					// 빈 블록이 아니라면 에러 Sound를 내고 종료합니다.
					if (cube[i].GetComponent<SpriteRenderer>().sprite != colorbox[0]) {
						this.audio.clip = this.sounds[1];
						this.audio.Play();
						return ;
					}
					// 포인터 색깔에 따라서 큐브 색을 변화시키고 포인터도 다음 색으로 바꿉니다.
					switch(color) {
						case "red":
							color = "orange";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[1];
							Check(1, i);
							break;
						case "orange":
							color = "yellow";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[2];
							Check(2, i);
							break;
						case "yellow":
							color = "green";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[3];
							Check(3, i);
							break;
						case "green":
							color = "blue";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[4];
							Check(4, i);
							break;
						case "blue":
							color = "indigo";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[5];
							Check(5, i);
							break;
						case "indigo":
							color = "purple";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[6];
							Check(6, i);
							break;
						case "purple":
							color = "red";
							cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[7];
							Check(7, i);
							break;
					}
				}
			}
		}
		// 4방향 화살표일 때
		if(	Input.GetKeyDown(KeyCode.UpArrow)	 || Input.GetKeyDown(KeyCode.DownArrow) ||
			Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			// 움직이는 소리로 설정
			this.audio.clip = this.sounds[0];
			// 위쪽 방향일 때
			if(Input.GetKeyDown(KeyCode.UpArrow)) {
				// 맨 윗칸일 때는 실행되지 않고 Invoke() 함수만 실행합니다.
				if (spotPos.y == 450f) {
					spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, spRender.color.a - 0.3f);
					Invoke("Invoke", 0.1f);
					return ;
				}
				// 위치를 이동합니다.
				transform.position = new Vector3(
					spotPos.x,
					spotPos.y + 50f,
					spotPos.z
				);
				// 이동 사운드를 출력하고 위치 포인터를 감소시킵니다.
				this.audio.Play();
				--location[0];
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)) {
				if (spotPos.y <= 0f) {
					spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, spRender.color.a - 0.3f);
					Invoke("Invoke", 0.1f);
					return ;
				}
				transform.position = new Vector3(
					spotPos.x,
					spotPos.y - 50f,
					spotPos.z
				);
				this.audio.Play();
				++location[0];
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)) {
				if (spotPos.x == 451f) {
					spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, spRender.color.a - 0.3f);
					Invoke("Invoke", 0.1f);
					return ;
				}
				transform.position = new Vector3(
					spotPos.x + 50f,
					spotPos.y,
					spotPos.z
				);
				this.audio.Play();
				++location[1];
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				if (spotPos.x == 1f) {
					spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, spRender.color.a - 0.3f);
					Invoke("Invoke", 0.1f);
					return ;
				}
				transform.position = new Vector3(
					spotPos.x - 50f,
					spotPos.y,
					spotPos.z 
				);
				this.audio.Play();
				--location[1];
			}
		}
	}

	private void			Invoke() { // Invoke 함수는 색을 잠깐 흐리게 한 뒤 에러 사운드를 내고 종료합니다.
		spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, spRender.color.a + 0.3f);
		this.audio.clip = this.sounds[1];
		this.audio.Play();
    }

	private void			Color() { // 포인터 색깔 설정
		switch(color) {
			case "red":
				spRender.color = new Color(1.000f, 0.510f, 0.510f, 0.700f);
				break;
			case "orange":
				spRender.color = new Color(1.000f, 0.630f, 0.310f, 0.700f);
				break;
			case "yellow":
				spRender.color = new Color(1.000f, 1.000f, 0.550f, 0.700f);
				break;
			case "green":
				spRender.color = new Color(0.550f, 1.000f, 0.600f, 0.700f);
				break;
			case "blue":
				spRender.color = new Color(0.550f, 0.850f, 1.000f, 0.700f);
				break;
			case "indigo":
				spRender.color = new Color(0.350f, 0.350f, 1.000f, 0.700f);
				break;
			case "purple":
				spRender.color = new Color(0.800f, 0.550f, 1.000f, 0.700f);
				break;
		}
	}

	public	void			Check(int colornum, int index) {
		checkbox = new int[101];
		for (int i=0; i<100; i++)
			checkbox[i] = 0;
		checkbox[index] = 1;
		index = index;
		Recur(colornum, index);
		double count = 0.0;
		for (int i=0; i<100; i++)
			if (checkbox[i] == 1)
				++count;
		if (count >= 3.0) {
			point += 2.0 + ((count - 3) / 2);
			// 블록 쌓는 소리를 개수에 따라 결정합니다.
			this.audio.clip = this.sounds[(int)((count - 3) + 2)];
			for (int i=0; i<100; i++)
				if (checkbox[i] == 1)
					cube[i].GetComponent<SpriteRenderer>().sprite = colorbox[0];
		}
		// 블록 쌓는 소리를 출력합니다.
		this.audio.Play();
	}

	private void			Recur(int colornum, int index) {
		if (cube[index].GetComponent<SpriteRenderer>().sprite == colorbox[colornum]) {
			checkbox[index] = 1;
			if (index > 09 && checkbox[index - 10] == 0)
				Recur(colornum, index - 10);
			if ((index % 10) > 0 && checkbox[index - 01] == 0)
				Recur(colornum, index - 01);
			if (index < 90 && checkbox[index + 10] == 0)
				Recur(colornum, index + 10);
			if ((index % 10) < 9 && checkbox[index + 01] == 0)
				Recur(colornum, index + 01);
		}
	}
}