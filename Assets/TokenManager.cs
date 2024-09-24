using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using System;

public class TokenManager : MonoBehaviour
{
    public string Address { get; private set; }
    public Button addColBtn;
    public Button replayBtn;
    public Button menuBtn;
    public Button rewind;
    public Button tokenForRewaidBtn;

    public Text ClaimingStatusText;

    private string receiverAddress = "0xb5A4FB8F5aFC725113bEE5c9Fc99f52059D6256F";

    private void Start()
    {
        ClaimingStatusText.gameObject.SetActive(false);
    }

    private static int ConvertStringToRoundedInt(string numberStr)
    {
        // Convert the string to a double
        double number = double.Parse(numberStr);

        // Round the number
        double roundedNumber = Math.Round(number);

        // Convert to int and return
        return (int)roundedNumber;
    }

    public async void SpendTokenToAddColumn()
    {
        if (GameManager.Instance.isAddBox == true) return;
        if (GameManager.Instance.currentLevel == 0) return;

        addColBtn.interactable = false;
        replayBtn.interactable = false;
        menuBtn.interactable = false;
        rewind.interactable = false;
        tokenForRewaidBtn.interactable = false;

        
        ClaimingStatusText.text = "Cost: 0.5 XFI";
        ClaimingStatusText.gameObject.SetActive(true);

        var userBalance = await ThirdwebManager.Instance.SDK.Wallet.GetBalance();
        if (ConvertStringToRoundedInt(userBalance.displayValue) < 1)
        {
            ClaimingStatusText.text = "Not Enough XFI";
        }
        else {
            try
            {
                // Thực hiện chuyển tiền, nếu thành công thì tiếp tục xử lý giao diện
                await ThirdwebManager.Instance.SDK.Wallet.Transfer(receiverAddress, "0.5");

                // Chỉ thực hiện các thay đổi giao diện nếu chuyển tiền thành công
                GUIManager.Instance.WatchVideo3();
                addColBtn.interactable = true;
                replayBtn.interactable = true;
                menuBtn.interactable = true;
                rewind.interactable = true;
                tokenForRewaidBtn.interactable = true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                Debug.LogError($"Lỗi khi thực hiện chuyển tiền: {ex.Message}");
            }
        }        
    }

    public async void SpendTokenToAddRewind()
    {
        addColBtn.interactable = false;
        replayBtn.interactable = false;
        menuBtn.interactable = false;
        rewind.interactable = false;
        tokenForRewaidBtn.interactable = false;

        ClaimingStatusText.text = "Cost: 0.5 XFI";
        ClaimingStatusText.gameObject.SetActive(true);

        var userBalance = await ThirdwebManager.Instance.SDK.Wallet.GetBalance();
        if (ConvertStringToRoundedInt(userBalance.displayValue) < 1)
        {
            ClaimingStatusText.text = "Not Enough XFI";
        }
        else
        {
            try
            {
                // Thực hiện chuyển tiền, nếu thành công thì tiếp tục xử lý giao diện
                await ThirdwebManager.Instance.SDK.Wallet.Transfer(receiverAddress, "0.5");

                // Chỉ thực hiện các thay đổi giao diện nếu chuyển tiền thành công
                GUIManager.Instance.WatchVideo2();
                tokenForRewaidBtn.gameObject.SetActive(false);
                addColBtn.interactable = true;
                replayBtn.interactable = true;
                menuBtn.interactable = true;
                rewind.interactable = true;
                tokenForRewaidBtn.interactable = true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                Debug.LogError($"Lỗi khi thực hiện chuyển tiền: {ex.Message}");
            }
        }       
    }
}
