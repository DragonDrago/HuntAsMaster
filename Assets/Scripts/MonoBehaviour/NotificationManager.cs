using System;
using System.Collections;
using System.Collections.Generic;
using NotificationSamples;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    private const string ChannelId = "game_channel0";
    private const string ReminderChannelId = "reminder_channel1";
    private const string NewsChannelId = "news_channel2";

    private GameNotificationsManager manager;

    private void Start()
    {
        manager = GetComponent<GameNotificationsManager>();

        var c1 = new GameNotificationChannel(ChannelId, "Default Game Channel", "Generic notifications");
        var c2 = new GameNotificationChannel(NewsChannelId, "News Channel", "News feed notifications");
        var c3 = new GameNotificationChannel(ReminderChannelId, "Reminder Channel", "Reminder notifications");

        manager.Initialize(c1, c2, c3);

        SetNotification();
    }

    private void SetNotification()
    {
        SendNotification("Hunt as Master",
                         "Hey, I'm waiting for you!",
                         DateTime.Now.AddDays(1),
                         1, true, ReminderChannelId, "icon_0", "icon_1"
            );

        SendNotification("Hunt as Master",
                         "Dont leave me alone ! I need you my hero, it is very critical mission!",
                         DateTime.Now.AddDays(3),
                         2, true, ReminderChannelId, "icon_0", "icon_1"
            );

        SendNotification("Hunt as Master",
                         "It seems you have forgotten me ! It has been so long when I saw you!",
                         DateTime.Now.AddDays(7),
                         3, true, ReminderChannelId, "icon_0", "icon_1"
            );
    }

    private void SendNotification(string title, string body, DateTime deliveryTime, int? badgeNumber = null,
            bool reschedule = false, string channelId = null,
            string smallIcon = null, string largeIcon = null)
    {
        ClearAllNotification();

        IGameNotification notification = manager.CreateNotification();

        if (notification == null)
        {
            return;
        }

        notification.Title = title;
        notification.Body = body;
        notification.Group = !string.IsNullOrEmpty(channelId) ? channelId : ChannelId;
        notification.DeliveryTime = deliveryTime;
        notification.SmallIcon = smallIcon;
        notification.LargeIcon = largeIcon;
        if (badgeNumber != null)
        {
            notification.BadgeNumber = badgeNumber;
        }

        PendingNotification notificationToDisplay = manager.ScheduleNotification(notification);
        notificationToDisplay.Reschedule = reschedule;
    }

    private void ClearAllNotification()
    {
        foreach (PendingNotification scheduledNotification in manager.PendingNotifications.ToArray())
        {
            CancelPendingNotificationItem(scheduledNotification);
        }
    }

    private void CancelPendingNotificationItem(PendingNotification itemToCancel)
    {
        manager.CancelNotification(itemToCancel.Notification.Id.Value);
    }



}
