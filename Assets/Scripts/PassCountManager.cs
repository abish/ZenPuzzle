using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassCountManager : Singleton<PassCountManager> {

	[SerializeField]
	public  int maxPassCount = 3;
	private int passCountRecoverInterval = 300;//sec
	private int _passCountRecoveredAt = -1;//epoch time
	private bool isDirty = true;//epoch time

    public int GetPassCountRecoveredAt () {
        // Initialize
        if (this._passCountRecoveredAt == -1 || this.isDirty == true)
        {
            this._passCountRecoveredAt = PlayerPrefs.GetInt("passCountRecoveredAt");
            this.isDirty = false;
        }

        return this._passCountRecoveredAt;
    }


    public int RestTimeToRecoverAll () {
        int now = DateUtil.GetEpochTime();
        int passCountRecoveredAt = this.GetPassCountRecoveredAt();
        // already recovered or never used before
        if (now >= passCountRecoveredAt)
            return 0;
       
       return passCountRecoveredAt - now; 
    }

    public int RestTimeToRecoverOne () {
        int restTimeToRecoverAll = this.RestTimeToRecoverAll();
        if (restTimeToRecoverAll <= 0)
            return 0;

        return restTimeToRecoverAll % this.passCountRecoverInterval;
    }


    public int GetValidPassCount () {
        int restTimeToRecoverAll = this.RestTimeToRecoverAll();
        if (restTimeToRecoverAll <= 0)
            return this.maxPassCount;

        int recoveredPassCount = (int)(this.maxPassCount - (float)restTimeToRecoverAll / this.passCountRecoverInterval);
        if (recoveredPassCount <= 0)
            return 0;

        return recoveredPassCount;
    }

    public bool CanExecPass () {
       return this.GetValidPassCount() > 0;
    }


    // return true when success. return false if not
    public bool ExecPass () {
        if (this.CanExecPass() == false)
            return false;
    
        int now = DateUtil.GetEpochTime();
        int passCountRecoveredAt = this.GetPassCountRecoveredAt();
        // initialize
        if (passCountRecoveredAt < now)
            passCountRecoveredAt = now;

        passCountRecoveredAt = passCountRecoveredAt + this.passCountRecoverInterval;
        this._passCountRecoveredAt = passCountRecoveredAt;
        PlayerPrefs.SetInt("passCountRecoveredAt", passCountRecoveredAt);
        PlayerPrefs.Save();
        return true;
    }

    public void SetDirty ()
    {
        this.isDirty = true;
    }

    public void RecoverPassCount ()
    {
        this.RecoverPassCount(1);
    }
    public void RecoverPassCount (int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning("invalid recover count" + count);
            return;
        }

        int restTimeToRecoverAll = this.RestTimeToRecoverAll();
        if (restTimeToRecoverAll <= 0)
            return;

        this._passCountRecoveredAt = this._passCountRecoveredAt - this.passCountRecoverInterval * count;
        PlayerPrefs.SetInt("passCountRecoveredAt", this._passCountRecoveredAt);
        PlayerPrefs.Save();
    }

}
