//
// Auto Generated Code By excel2json
// https://neil3d.gitee.io/coding/excel2json.html
// 1. 每个 Sheet 形成一个 Struct 定义, Sheet 的名称作为 Struct 的名称
// 2. 表格约定：第一行是变量名称，第二行是变量类型

// Generate From C:\Users\jnsw\Desktop\Description.xlsx.xlsx
using System;
using System.Collections.Generic;

[Serializable]
	public class Sheet1
	{
	public int ID; // 编号
	public string Name; // 名称
	public string Description; // 描述
	public string Name_en; // 名称（en）
	public string Des_en; // 描述（en）
	public string Name_es; // 
	public string Des_es; // 
	public string Name_ru; // 
	public string Des_ru; // 
	}

public class Description {
	public List<Sheet1> des;
}