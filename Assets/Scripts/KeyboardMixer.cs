using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardMixer : MonoBehaviour
{
    public Dictionary<char, char> charMap;
    public MixedKeyboard[] mixedKeyboards;
    public AudioSource audioSource;
    public AudioClip correct;
    public AudioClip wrong;
    private Text text;
    private GameManager gameManager;

    private readonly string keyboardMixerDocs = "Key,Letter\n" +
    "Q,U\n" +
    "W,B\n" +
    "E,Y\n" +
    "R,G\n" +
    "T,W\n" +
    "Y,V\n" +
    "U,Q\n" +
    "I,R\n" +
    "O,S\n" +
    "P,T\n" +
    "A,M\n" +
    "S,C\n" +
    "D,E\n" +
    "F,K\n" +
    "G,L\n" +
    "H,F\n" +
    "J,I\n" +
    "K,X\n" +
    "L,H\n" +
    "Z,Z\n" +
    "X,D\n" +
    "C,J\n" +
    "V,O\n" +
    "B,A\n" +
    "N,P\n" +
    "M,N\n" +
    "Key,Letter\n" +
    "Q,G\n" +
    "W,J\n" +
    "E,A\n" +
    "R,R\n" +
    "T,P\n" +
    "Y,T\n" +
    "U,C\n" +
    "I,E\n" +
    "O,Y\n" +
    "P,N\n" +
    "A,K\n" +
    "S,X\n" +
    "D,W\n" +
    "F,H\n" +
    "G,Q\n" +
    "H,L\n" +
    "J,D\n" +
    "K,B\n" +
    "L,M\n" +
    "Z,F\n" +
    "X,O\n" +
    "C,S\n" +
    "V,I\n" +
    "B,Z\n" +
    "N,V\n" +
    "M,U\n" +
    "Key,Letter\n" +
    "Q,W\n" +
    "W,R\n" +
    "E,Q\n" +
    "R,A\n" +
    "T,G\n" +
    "Y,M\n" +
    "U,N\n" +
    "I,B\n" +
    "O,L\n" +
    "P,C\n" +
    "A,I\n" +
    "S,X\n" +
    "D,E\n" +
    "F,Y\n" +
    "G,H\n" +
    "H,Z\n" +
    "J,K\n" +
    "K,O\n" +
    "L,V\n" +
    "Z,D\n" +
    "X,F\n" +
    "C,P\n" +
    "V,S\n" +
    "B,U\n" +
    "N,J\n" +
    "M,T\n" +
    "Key,Letter\n" +
    "Q,X\n" +
    "W,Z\n" +
    "E,J\n" +
    "R,H\n" +
    "T,B\n" +
    "Y,D\n" +
    "U,G\n" +
    "I,S\n" +
    "O,F\n" +
    "P,W\n" +
    "A,I\n" +
    "S,R\n" +
    "D,E\n" +
    "F,C\n" +
    "G,O\n" +
    "H,L\n" +
    "J,P\n" +
    "K,A\n" +
    "L,Y\n" +
    "Z,K\n" +
    "X,M\n" +
    "C,U\n" +
    "V,V\n" +
    "B,T\n" +
    "N,N\n" +
    "M,Q\n" +
    "Key,Letter\n" +
    "Q,O\n" +
    "W,A\n" +
    "E,S\n" +
    "R,P\n" +
    "T,J\n" +
    "Y,U\n" +
    "U,V\n" +
    "I,D\n" +
    "O,L\n" +
    "P,Y\n" +
    "A,B\n" +
    "S,I\n" +
    "D,T\n" +
    "F,R\n" +
    "G,M\n" +
    "H,X\n" +
    "J,F\n" +
    "K,W\n" +
    "L,H\n" +
    "Z,N\n" +
    "X,K\n" +
    "C,Q\n" +
    "V,G\n" +
    "B,Z\n" +
    "N,C\n" +
    "M,E\n" +
    "Key,Letter\n" +
    "Q,L\n" +
    "W,B\n" +
    "E,Y\n" +
    "R,J\n" +
    "T,E\n" +
    "Y,M\n" +
    "U,D\n" +
    "I,Q\n" +
    "O,I\n" +
    "P,X\n" +
    "A,P\n" +
    "S,F\n" +
    "D,T\n" +
    "F,O\n" +
    "G,A\n" +
    "H,H\n" +
    "J,W\n" +
    "K,Z\n" +
    "L,G\n" +
    "Z,U\n" +
    "X,K\n" +
    "C,S\n" +
    "V,V\n" +
    "B,N\n" +
    "N,R\n" +
    "M,C\n" +
    "Key,Letter\n" +
    "Q,B\n" +
    "W,G\n" +
    "E,X\n" +
    "R,E\n" +
    "T,I\n" +
    "Y,F\n" +
    "U,N\n" +
    "I,Z\n" +
    "O,S\n" +
    "P,D\n" +
    "A,V\n" +
    "S,C\n" +
    "D,U\n" +
    "F,L\n" +
    "G,A\n" +
    "H,O\n" +
    "J,J\n" +
    "K,T\n" +
    "L,K\n" +
    "Z,Q\n" +
    "X,M\n" +
    "C,Y\n" +
    "V,H\n" +
    "B,W\n" +
    "N,R\n" +
    "M,P\n" +
    "Key,Letter\n" +
    "Q,U\n" +
    "W,J\n" +
    "E,W\n" +
    "R,I\n" +
    "T,E\n" +
    "Y,P\n" +
    "U,M\n" +
    "I,R\n" +
    "O,B\n" +
    "P,N\n" +
    "A,S\n" +
    "S,K\n" +
    "D,L\n" +
    "F,Q\n" +
    "G,D\n" +
    "H,F\n" +
    "J,Y\n" +
    "K,T\n" +
    "L,G\n" +
    "Z,C\n" +
    "X,Z\n" +
    "C,H\n" +
    "V,O\n" +
    "B,V\n" +
    "N,X\n" +
    "M,A\n" +
    "Key,Letter\n" +
    "Q,M\n" +
    "W,O\n" +
    "E,W\n" +
    "R,F\n" +
    "T,S\n" +
    "Y,G\n" +
    "U,C\n" +
    "I,I\n" +
    "O,P\n" +
    "P,T\n" +
    "A,Z\n" +
    "S,J\n" +
    "D,A\n" +
    "F,Y\n" +
    "G,B\n" +
    "H,L\n" +
    "J,U\n" +
    "K,N\n" +
    "L,R\n" +
    "Z,H\n" +
    "X,X\n" +
    "C,Q\n" +
    "V,E\n" +
    "B,D\n" +
    "N,K\n" +
    "M,V\n" +
    "Key,Letter\n" +
    "Q,J\n" +
    "W,E\n" +
    "E,W\n" +
    "R,D\n" +
    "T,A\n" +
    "Y,S\n" +
    "U,Q\n" +
    "I,O\n" +
    "O,K\n" +
    "P,P\n" +
    "A,V\n" +
    "S,X\n" +
    "D,F\n" +
    "F,U\n" +
    "G,Y\n" +
    "H,B\n" +
    "J,L\n" +
    "K,N\n" +
    "L,H\n" +
    "Z,R\n" +
    "X,Z\n" +
    "C,T\n" +
    "V,C\n" +
    "B,I\n" +
    "N,G\n" +
    "M,M\n" +
    "Key,Letter\n" +
    "Q,M\n" +
    "W,D\n" +
    "E,W\n" +
    "R,F\n" +
    "T,E\n" +
    "Y,I\n" +
    "U,C\n" +
    "I,P\n" +
    "O,R\n" +
    "P,Y\n" +
    "A,G\n" +
    "S,T\n" +
    "D,N\n" +
    "F,U\n" +
    "G,A\n" +
    "H,S\n" +
    "J,Z\n" +
    "K,K\n" +
    "L,H\n" +
    "Z,V\n" +
    "X,J\n" +
    "C,X\n" +
    "V,L\n" +
    "B,B\n" +
    "N,Q\n" +
    "M,O\n" +
    "Key,Letter\n" +
    "Q,L\n" +
    "W,A\n" +
    "E,G\n" +
    "R,F\n" +
    "T,D\n" +
    "Y,K\n" +
    "U,C\n" +
    "I,E\n" +
    "O,S\n" +
    "P,X\n" +
    "A,J\n" +
    "S,Y\n" +
    "D,V\n" +
    "F,U\n" +
    "G,N\n" +
    "H,B\n" +
    "J,P\n" +
    "K,M\n" +
    "L,Q\n" +
    "Z,O\n" +
    "X,H\n" +
    "C,W\n" +
    "V,I\n" +
    "B,T\n" +
    "N,Z\n" +
    "M,R\n" +
    "Key,Letter\n" +
    "Q,O\n" +
    "W,P\n" +
    "E,Z\n" +
    "R,Y\n" +
    "T,A\n" +
    "Y,R\n" +
    "U,L\n" +
    "I,U\n" +
    "O,M\n" +
    "P,K\n" +
    "A,W\n" +
    "S,X\n" +
    "D,I\n" +
    "F,S\n" +
    "G,G\n" +
    "H,T\n" +
    "J,D\n" +
    "K,V\n" +
    "L,N\n" +
    "Z,Q\n" +
    "X,F\n" +
    "C,E\n" +
    "V,J\n" +
    "B,H\n" +
    "N,B\n" +
    "M,C\n" +
    "Key,Letter\n" +
    "Q,Z\n" +
    "W,E\n" +
    "E,N\n" +
    "R,X\n" +
    "T,H\n" +
    "Y,Y\n" +
    "U,U\n" +
    "I,G\n" +
    "O,I\n" +
    "P,S\n" +
    "A,F\n" +
    "S,P\n" +
    "D,D\n" +
    "F,B\n" +
    "G,V\n" +
    "H,Q\n" +
    "J,M\n" +
    "K,T\n" +
    "L,A\n" +
    "Z,W\n" +
    "X,L\n" +
    "C,J\n" +
    "V,R\n" +
    "B,K\n" +
    "N,C\n" +
    "M,O\n" +
    "Key,Letter\n" +
    "Q,T\n" +
    "W,Q\n" +
    "E,B\n" +
    "R,I\n" +
    "T,E\n" +
    "Y,D\n" +
    "U,O\n" +
    "I,F\n" +
    "O,K\n" +
    "P,C\n" +
    "A,H\n" +
    "S,Y\n" +
    "D,G\n" +
    "F,J\n" +
    "G,N\n" +
    "H,A\n" +
    "J,P\n" +
    "K,L\n" +
    "L,V\n" +
    "Z,R\n" +
    "X,W\n" +
    "C,M\n" +
    "V,S\n" +
    "B,U\n" +
    "N,X\n" +
    "M,Z\n" +
    "Key,Letter\n" +
    "Q,V\n" +
    "W,M\n" +
    "E,P\n" +
    "R,I\n" +
    "T,W\n" +
    "Y,O\n" +
    "U,G\n" +
    "I,U\n" +
    "O,Y\n" +
    "P,R\n" +
    "A,J\n" +
    "S,L\n" +
    "D,T\n" +
    "F,A\n" +
    "G,Q\n" +
    "H,D\n" +
    "J,S\n" +
    "K,F\n" +
    "L,C\n" +
    "Z,X\n" +
    "X,E\n" +
    "C,Z\n" +
    "V,H\n" +
    "B,B\n" +
    "N,N\n" +
    "M,K\n" +
    "Key,Letter\n" +
    "Q,D\n" +
    "W,Y\n" +
    "E,T\n" +
    "R,A\n" +
    "T,J\n" +
    "Y,O\n" +
    "U,H\n" +
    "I,L\n" +
    "O,V\n" +
    "P,C\n" +
    "A,E\n" +
    "S,Q\n" +
    "D,G\n" +
    "F,P\n" +
    "G,K\n" +
    "H,Z\n" +
    "J,X\n" +
    "K,U\n" +
    "L,S\n" +
    "Z,I\n" +
    "X,B\n" +
    "C,M\n" +
    "V,W\n" +
    "B,F\n" +
    "N,N\n" +
    "M,R\n" +
    "Key,Letter\n" +
    "Q,B\n" +
    "W,Q\n" +
    "E,E\n" +
    "R,R\n" +
    "T,U\n" +
    "Y,H\n" +
    "U,D\n" +
    "I,Z\n" +
    "O,N\n" +
    "P,G\n" +
    "A,F\n" +
    "S,A\n" +
    "D,V\n" +
    "F,L\n" +
    "G,S\n" +
    "H,T\n" +
    "J,J\n" +
    "K,W\n" +
    "L,Y\n" +
    "Z,P\n" +
    "X,X\n" +
    "C,O\n" +
    "V,C\n" +
    "B,M\n" +
    "N,I\n" +
    "M,K\n" +
    "Key,Letter\n" +
    "Q,Z\n" +
    "W,E\n" +
    "E,A\n" +
    "R,W\n" +
    "T,K\n" +
    "Y,G\n" +
    "U,T\n" +
    "I,Y\n" +
    "O,L\n" +
    "P,J\n" +
    "A,N\n" +
    "S,O\n" +
    "D,H\n" +
    "F,M\n" +
    "G,U\n" +
    "H,R\n" +
    "J,F\n" +
    "K,B\n" +
    "L,D\n" +
    "Z,S\n" +
    "X,Q\n" +
    "C,X\n" +
    "V,V\n" +
    "B,P\n" +
    "N,C\n" +
    "M,I\n" +
    "Key,Letter\n" +
    "Q,Z\n" +
    "W,Q\n" +
    "E,A\n" +
    "R,E\n" +
    "T,W\n" +
    "Y,X\n" +
    "U,R\n" +
    "I,O\n" +
    "O,M\n" +
    "P,S\n" +
    "A,K\n" +
    "S,T\n" +
    "D,B\n" +
    "F,P\n" +
    "G,J\n" +
    "H,G\n" +
    "J,V\n" +
    "K,U\n" +
    "L,Y\n" +
    "Z,L\n" +
    "X,N\n" +
    "C,H\n" +
    "V,F\n" +
    "B,I\n" +
    "N,D\n" +
    "M,C\n" +
    "Key,Letter\n" +
    "Q,F\n" +
    "W,A\n" +
    "E,Y\n" +
    "R,E\n" +
    "T,I\n" +
    "Y,K\n" +
    "U,T\n" +
    "I,S\n" +
    "O,W\n" +
    "P,Z\n" +
    "A,C\n" +
    "S,V\n" +
    "D,U\n" +
    "F,B\n" +
    "G,Q\n" +
    "H,O\n" +
    "J,G\n" +
    "K,M\n" +
    "L,J\n" +
    "Z,R\n" +
    "X,D\n" +
    "C,N\n" +
    "V,H\n" +
    "B,P\n" +
    "N,X\n" +
    "M,L\n" +
    "Key,Letter\n" +
    "Q,O\n" +
    "W,J\n" +
    "E,F\n" +
    "R,E\n" +
    "T,D\n" +
    "Y,A\n" +
    "U,K\n" +
    "I,Q\n" +
    "O,G\n" +
    "P,X\n" +
    "A,S\n" +
    "S,C\n" +
    "D,H\n" +
    "F,R\n" +
    "G,V\n" +
    "H,U\n" +
    "J,T\n" +
    "K,P\n" +
    "L,B\n" +
    "Z,Z\n" +
    "X,W\n" +
    "C,I\n" +
    "V,Y\n" +
    "B,L\n" +
    "N,M\n" +
    "M,N\n" +
    "Key,Letter\n" +
    "Q,P\n" +
    "W,L\n" +
    "E,M\n" +
    "R,D\n" +
    "T,C\n" +
    "Y,F\n" +
    "U,K\n" +
    "I,U\n" +
    "O,J\n" +
    "P,N\n" +
    "A,H\n" +
    "S,V\n" +
    "D,Y\n" +
    "F,I\n" +
    "G,T\n" +
    "H,W\n" +
    "J,B\n" +
    "K,O\n" +
    "L,R\n" +
    "Z,Z\n" +
    "X,S\n" +
    "C,E\n" +
    "V,G\n" +
    "B,A\n" +
    "N,X\n" +
    "M,Q\n" +
    "Key,Letter\n" +
    "Q,R\n" +
    "W,H\n" +
    "E,P\n" +
    "R,M\n" +
    "T,B\n" +
    "Y,S\n" +
    "U,K\n" +
    "I,V\n" +
    "O,O\n" +
    "P,I\n" +
    "A,T\n" +
    "S,X\n" +
    "D,Z\n" +
    "F,G\n" +
    "G,E\n" +
    "H,D\n" +
    "J,J\n" +
    "K,Q\n" +
    "L,A\n" +
    "Z,U\n" +
    "X,L\n" +
    "C,C\n" +
    "V,F\n" +
    "B,N\n" +
    "N,W\n" +
    "M,Y\n" +
    "Key,Letter\n" +
    "Q,C\n" +
    "W,R\n" +
    "E,D\n" +
    "R,O\n" +
    "T,P\n" +
    "Y,L\n" +
    "U,G\n" +
    "I,S\n" +
    "O,A\n" +
    "P,B\n" +
    "A,Y\n" +
    "S,T\n" +
    "D,N\n" +
    "F,K\n" +
    "G,E\n" +
    "H,I\n" +
    "J,Q\n" +
    "K,M\n" +
    "L,X\n" +
    "Z,J\n" +
    "X,U\n" +
    "C,V\n" +
    "V,F\n" +
    "B,W\n" +
    "N,H\n" +
    "M,Z\n" +
    "Key,Letter\n" +
    "Q,X\n" +
    "W,U\n" +
    "E,C\n" +
    "R,E\n" +
    "T,K\n" +
    "Y,D\n" +
    "U,W\n" +
    "I,M\n" +
    "O,N\n" +
    "P,P\n" +
    "A,O\n" +
    "S,Q\n" +
    "D,H\n" +
    "F,G\n" +
    "G,B\n" +
    "H,L\n" +
    "J,Y\n" +
    "K,R\n" +
    "L,V\n" +
    "Z,S\n" +
    "X,T\n" +
    "C,J\n" +
    "V,F\n" +
    "B,I\n" +
    "N,A\n" +
    "M,Z\n" +
    "Key,Letter\n" +
    "Q,G\n" +
    "W,R\n" +
    "E,C\n" +
    "R,B\n" +
    "T,H\n" +
    "Y,A\n" +
    "U,I\n" +
    "I,U\n" +
    "O,L\n" +
    "P,J\n" +
    "A,D\n" +
    "S,Y\n" +
    "D,Z\n" +
    "F,M\n" +
    "G,N\n" +
    "H,F\n" +
    "J,S\n" +
    "K,P\n" +
    "L,X\n" +
    "Z,T\n" +
    "X,K\n" +
    "C,V\n" +
    "V,W\n" +
    "B,E\n" +
    "N,Q\n" +
    "M,O\n" +
    "Key,Letter\n" +
    "Q,N\n" +
    "W,Z\n" +
    "E,A\n" +
    "R,R\n" +
    "T,Q\n" +
    "Y,G\n" +
    "U,L\n" +
    "I,S\n" +
    "O,E\n" +
    "P,H\n" +
    "A,F\n" +
    "S,T\n" +
    "D,K\n" +
    "F,J\n" +
    "G,W\n" +
    "H,I\n" +
    "J,B\n" +
    "K,X\n" +
    "L,U\n" +
    "Z,O\n" +
    "X,Y\n" +
    "C,V\n" +
    "V,M\n" +
    "B,P\n" +
    "N,D\n" +
    "M,C\n" +
    "Key,Letter\n" +
    "Q,M\n" +
    "W,Z\n" +
    "E,V\n" +
    "R,I\n" +
    "T,F\n" +
    "Y,X\n" +
    "U,P\n" +
    "I,K\n" +
    "O,H\n" +
    "P,T\n" +
    "A,C\n" +
    "S,O\n" +
    "D,B\n" +
    "F,R\n" +
    "G,D\n" +
    "H,E\n" +
    "J,J\n" +
    "K,A\n" +
    "L,S\n" +
    "Z,L\n" +
    "X,N\n" +
    "C,Q\n" +
    "V,Y\n" +
    "B,G\n" +
    "N,U\n" +
    "M,W\n" +
    "Key,Letter\n" +
    "Q,O\n" +
    "W,Y\n" +
    "E,N\n" +
    "R,I\n" +
    "T,K\n" +
    "Y,R\n" +
    "U,F\n" +
    "I,Z\n" +
    "O,S\n" +
    "P,P\n" +
    "A,G\n" +
    "S,D\n" +
    "D,A\n" +
    "F,B\n" +
    "G,Q\n" +
    "H,V\n" +
    "J,C\n" +
    "K,E\n" +
    "L,M\n" +
    "Z,U\n" +
    "X,L\n" +
    "C,H\n" +
    "V,W\n" +
    "B,X\n" +
    "N,J\n" +
    "M,T";

    public bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        string[] keyMapDocs = keyboardMixerDocs.Split(new string[] { "Key,Letter\n" }, System.StringSplitOptions.None);
        for (int i = 0; i < mixedKeyboards.Length; i++)
        {
            MixedKeyboard keyboard = mixedKeyboards[i];
            string keyMapDoc = keyMapDocs[i];
            keyboard.charMap = new Dictionary<char, char>();
            string[] lines = keyMapDoc.Split('\n');
            for (int j = 0; j < lines.Length - 1; j++)
            {
                string[] pair = lines[j].Trim().Split(',');
                keyboard.charMap[pair[0].ToCharArray()[0]] = pair[1].ToCharArray()[0];
            }
        }
        RemixKeyboard();
        text = GetComponent<Text>();
        isEnabled = true;
    }

    public void RemixKeyboard()
    {
        int keyboardIndex = Random.Range(0, mixedKeyboards.Length);
        Debug.Log(keyboardIndex);
        charMap = mixedKeyboards[keyboardIndex].charMap;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessNewKey();
    }

    public void ProcessNewKey()
    {
        if (!isEnabled)
        {
            return;
        }
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (text.text.Length != 0)
                {
                    text.text = text.text.Substring(0, text.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                ChooseSubmit();
            }
            else
            {
                text.text += GetInputChar(c);
            }
        }
    }

    public char GetInputChar(char newChar)
    {
        charMap.TryGetValue(char.ToUpper(newChar), out char realChar);
        if (realChar != default)
        {
            return realChar;
        }
        return newChar;
    }

    public void ChooseSubmit()
    {
        gameManager.ProcessSubmission(Submit());
    }

    public int Submit()
    {
        // Return error count
        // Return error count
        gameManager.feedback = "";
        string submission = text.text.ToUpper();
        string currentQuack = gameManager.currentQuack.ToUpper();
        if (submission.Equals(currentQuack))
        {
            gameManager.feedback = "Good job.";
            PlayCorrect();
            return 0;
        }
        int errorCount = 0;
        string[] submissionWords = submission.Split(' ');
        string[] currentQuackWords = currentQuack.Split(' ');
        foreach (string quackWord in currentQuackWords)
        {
            if (!submissionWords.Contains(quackWord))
            {
                if (gameManager.feedback.Equals(""))
                {
                    gameManager.feedback = string.Format("I said \"{0}\", intern. Are you even listening?", quackWord);
                }
                errorCount++;
            }
        }
        foreach (string submissionWord in submissionWords)
        {
            if (!currentQuackWords.Contains(submissionWord))
            {
                // TODO: compain about first error
                if (gameManager.feedback.Equals(""))
                {
                    gameManager.feedback = string.Format("Who told you to send \"{0}\"? Not me, intern. Not me.", submissionWord);
                }
                errorCount++;
            }
        }
        PlayWrong();
        return errorCount;
    }

    public void PlayCorrect()
    {
        audioSource.clip = correct;
        audioSource.Play();
    }

    public void PlayWrong()
    {
        audioSource.clip = wrong;
        audioSource.Play();
    }
}
