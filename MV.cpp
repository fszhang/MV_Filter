
byte AVG_DataTabCount = 20;
byte dataIndex = 0;
uint32_t dataArray[20];
byte AVG_Times = 20;
uint32_t dataLast = 0;

void AddData()
{
    uint32_t dataBase;
    uint32_t dataNoise;
    uint32_t dataInt = dataNoise + dataBase;
    DataAVG(dataIndex, &AVG_Times, dataArray, &dataInt, &dataLast);
}

void DataAVG(byte dataIndex, byte *AVG_Times, uint32_t dataTab[], uint32_t *dataIn, uint32_t *dataLast)
{
    byte i, dataPosition;
    uint32_t Sum;
    uint32_t PID_Data, PID_DataCurrent, Margin;
    dataTab[dataIndex] = PID_DataCurrent = *dataIn;
    for (i = 0, Sum = 0; i < *AVG_Times; i++)
    {
        if (dataIndex >= i)
            dataPosition = (byte)(dataIndex - i);
        else
            dataPosition = (byte)(AVG_DataTabCount + dataIndex - i);

        Sum += dataTab[dataPosition];
    }
    PID_Data = Sum / (*AVG_Times);
    Margin = PID_Data / 10;
    if (((PID_DataCurrent > (PID_Data + Margin)) && (*dataLast > (PID_Data + Margin))) ||
        (((PID_DataCurrent + Margin) < PID_Data) && ((*dataLast + Margin) < PID_Data)))
    {
        *AVG_Times = 2;
    }
    else
    {
        if (*AVG_Times < AVG_DataTabCount)
            AVG_Times++;
    }
    *dataIn = PID_Data;
    *dataLast = PID_DataCurrent;
}