#ifndef __TABLE_CONFIG_H
#define __TABLE_CONFIG_H
#include "BaseConfigEx.h"
struct table_behaviortreeT : ConfigBaseT
{
	virtual ~table_behaviortreeT()
	{
	}
	table_behaviortreeT()
	{
		FileName = "table_behaviortree.csv";
		_kf =
		{
			{"ID",{INT_FD, &ID}},
			{"EnName",{TSTR_FD, &EnName}},
			{"CnName",{TSTR_FD, &CnName}},
			{"Type",{INT_FD, &Type}},
			{"FloatValue",{FLOAT_FD, &FloatValue}},
			{"IntValue",{INT_FD, &IntValue}},
			{"BoolValue",{INT_FD, &BoolValue}},
			{"StringValue",{TSTR_FD, &StringValue}}
		};
	}
	virtual table_behaviortreeT* create()
	{
		return new table_behaviortreeT();
	}
	enum
	{
	};
	INT32 ID = 0;            //ID
	TStr EnName = "";				//英文名称
	TStr CnName = "";				//中文说明文字
	INT32 Type = 0;				//类型
	float FloatValue = 0.0f;				//float值
	INT32 IntValue = 0;				//int值
	INT32 BoolValue = 0;				//bool值
	TStr StringValue = "";				//string值
};
CreateCsvClass(table_behaviortree);

struct table_text_localizationT : ConfigBaseT
{
	virtual ~table_text_localizationT()
	{
	}
	table_text_localizationT()
	{
		FileName = "table_text_localization.csv";
		_kf =
		{
			{"ID",{INT_FD, &ID}},
			{"Key",{TSTR_FD, &Key}},
			{"CN",{TSTR_FD, &CN}},
			{"EN",{TSTR_FD, &EN}}
		};
	}
	virtual table_text_localizationT* create()
	{
		return new table_text_localizationT();
	}
	enum
	{
	};
	INT32 ID = 0;            //ID
	TStr Key = "";				//文本key
	TStr CN = "";				//Chinese
	TStr EN = "";				//English
};
CreateCsvClass(table_text_localization);

#endif