
public byte AVG_DataTabCount = 20;
private byte dataIndex = 0;
private UInt32[] dataArray = new UInt32[20];
private byte AVG_Times = 20;
private UInt32 dataLast = 0;
private System.Random dataNoise = new System.Random();


public void AddData()
{
    UInt32 dataBase = 10000;
    UInt32 dataInt = (UInt32)dataNoise.Next(1000) + dataBase;
    DataAVG(dataIndex, ref AVG_Times, ref dataArray, ref dataInt, ref dataLast);
}


private void DataAVG(byte dataIndex, ref byte AVG_Times, ref UInt32[] dataTab, ref UInt32 dataIn, ref UInt32 dataLast)
{
    byte i, dataPosition;
    UInt32 Sum;
    UInt32 PID_Data, PID_DataCurrent, Margin;
    dataTab[dataIndex] = PID_DataCurrent = dataIn;
    for (i = 0, Sum = 0; i < AVG_Times; i++)
    {
        if (dataIndex >= i)
            dataPosition = (byte)(dataIndex - i);
        else
            dataPosition = (byte)(AVG_DataTabCount + dataIndex - i);

        Sum += dataTab[dataPosition];
    }
    PID_Data = Sum / (AVG_Times);
    Margin = PID_Data / 10;
    if (((PID_DataCurrent > (PID_Data + Margin)) && (dataLast > (PID_Data + Margin))) ||
        (((PID_DataCurrent + Margin) < PID_Data) && ((dataLast + Margin) < PID_Data)))
    {
        AVG_Times = 2;
    }
    else
    {
        if (AVG_Times < AVG_DataTabCount)
            AVG_Times++;
    }
    dataIn = PID_Data;
    dataLast = PID_DataCurrent;
}