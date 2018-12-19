using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvRender : MonoBehaviour {

    public int StageNo = 1;
    private List<string[]> stageData = new List<string[]>();

    // Use this for initialization
    void Start () {

        var fileName = "stage_"+(StageNo).ToString();

        // Resourcesのcsvフォルダ内のcsvファイルをTextAssetとして取得
        var csvFile = Resources.Load("csv/" + fileName) as TextAsset;

        // csvファイルの内容をStringReaderに変換
        var reader = new StringReader(csvFile.text);

        // csvファイルの内容を一行ずつ末尾まで取得しリストを作成
        while (reader.Peek() > -1)
        {
            // 一行読み込む
            var lineData = reader.ReadLine();
            // カンマ(,)区切りのデータを文字列の配列に変換
            var stage = lineData.Split(',');
            // リストに追加
            stageData.Add(stage);
            // 末尾まで繰り返し...
        }

        // ログに読み込んだデータを表示する
        foreach (var data in stageData)
        {
            Debug.Log("DATA:" + data[0] + " / " + data[1] + " / " + data[2]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
