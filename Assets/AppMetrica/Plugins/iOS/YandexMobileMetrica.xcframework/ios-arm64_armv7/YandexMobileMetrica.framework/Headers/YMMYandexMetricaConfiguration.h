/*
 * Version for iOS
 * © 2012–2019 YANDEX
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * https://yandex.com/legal/appmetrica_sdk_agreement/
 */

#import <Foundation/Foundation.h>

@class CLLocation;
@class YMMYandexMetricaPreloadInfo;

NS_ASSUME_NONNULL_BEGIN

@interface YMMYandexMetricaConfiguration : NSObject

/** Initialize configuration with specified Application key.
 For invalid Application initialization returns nil in release and raises an exception in debug.

 @param apiKey Application key that is issued during application registration in AppMetrica.
 Application key must be a hexadecimal string in format xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.
 The key can be requested or checked at https://appmetrica.yandex.com
 */
- (nullable instancetype)initWithApiKey:(NSString *)apiKey;

- (instancetype)init __attribute__((unavailable("initWithApiKey: must be used instead.")));

/** Get Application key used to initialize the configuration.
 */
@property (nonatomic, copy, readonly) NSString *apiKey;

/** Whether first activation of AppMetrica should be considered as app update or new app install.
 If this option is enabled the first call of +[YMMYandexMetrica activateWithApiKey:] or
 +[YMMYandexMetrica activateWithConfiguration:] will be considered as an application update.

 By default this option is disabled.
 */
@property (nonatomic, assign) BOOL handleFirstActivationAsUpdate;

/** Whether activation of AppMetrica should be considered as the start of a session.
 If this option is disabled session starts at UIApplicationDidBecomeActiveNotification.

 The option is disabled by default. Enable this property if you want events that are reported after activation to join
 the current session.
 */
@property (nonatomic, assign) BOOL handleActivationAsSessionStart;

/** Whether AppMetrica should automatically track session starts and ends.
 AppMetrica uses UIApplicationDidBecomeActiveNotification and UIApplicationWillResignActiveNotification notifications
 to track sessions.

 The maximum length of the session is 24 hours. To continue the session after 24 hours, you should manually
 invoke the resumeSession method.

 The option is enabled by default. If the option is disabled, you should manually control the session
 using pauseSession and resumeSession methods.
 */
@property (nonatomic, assign) BOOL sessionsAutoTracking;

/** A boolean value indicating whether statistics sending to the AppMetrica server is enabled.

 @note Disabling this option also turns off data sending from the reporters that initialized for different apiKey.

 By default, the statistics sending is enabled.
 */
@property (nonatomic, assign) BOOL statisticsSending;

/** Maximum number of reports stored in the database.

 Acceptable values are in the range of [100; 10000]. If passed value is outside of the range, AppMetrica automatically
 trims it to closest border value.

 @note Different apiKeys use different databases and can have different limits of reports count.
 The parameter only affects the configuration created for that apiKey.
 To set the parameter for a different apiKey, see `YMMReporterConfiguration.maxReportsInDatabaseCount`

 By default, the parameter value is 1000.
 */
@property (nonatomic, assign) NSUInteger maxReportsInDatabaseCount;

/** Enable/disable location reporting to AppMetrica.
 If enabled and location set via setLocation: method - that location would be used.
 If enabled and location is not set via setLocation,
 but application has appropriate permission - CLLocationManager would be used to acquire location data.

 Enabled by default.
 */
@property (nonatomic, assign) BOOL locationTracking;

/** Set/get location to AppMetrica
 To enable AppMetrica to use this location trackLocationEnabled should be 'YES'

 By default is nil
 */
@property (nonatomic, strong, nullable) CLLocation *location;

/** Set/get session timeout (in seconds).
 Time limit before the application is considered inactive.
 Minimum accepted value is 10 seconds. All passed values below 10 seconds automatically become 10 seconds.

 By default, the session times out if the application is in background for 10 seconds.
 */
@property (nonatomic, assign) NSUInteger sessionTimeout;

/** Enable/disable tracking app crashes.

 Enabled by default.
 To disable crash tracking, set the parameter value to false.
 */
@property (nonatomic, assign) BOOL crashReporting;

/** Set/get the arbitrary application version for AppMetrica to report.

 By default, the application version is set in the app configuration file Info.plist (CFBundleShortVersionString).
 */
@property (nonatomic, copy, nullable) NSString *appVersion;

/** Enable/disable logging.

 By default logging is disabled.
 */
@property (nonatomic, assign) BOOL logs;

/** Defines the app type as "For Kids" to comply with the
 [App Store Review Guidelines for Kids' Category](https://developer.apple.com/app-store/review/guidelines/#kids).
 If the option is enabled, AppMetrica SDK doesn't send advertising IDs and device location.

@note Enable this option only if your app intended for the "For Kids" category.
 */
@property (nonatomic, assign) BOOL appForKids DEPRECATED_ATTRIBUTE;

/** Set/get preload info, which is used for tracking preload installs.
 Additional info could be https://appmetrica.yandex.com

 By default is nil.
 */
@property (nonatomic, copy, nullable) YMMYandexMetricaPreloadInfo *preloadInfo;

/**
 Enables/disables auto tracking of inapp purchases.

 By default is enabled.
 */
@property (nonatomic, assign) BOOL revenueAutoTrackingEnabled;

/**
 Enables/disables app open auto tracking.
 By default is enabled.

 Set this flag to YES to track URLs that open the app.
 @note Auto tracking will only capture links that open the app. Those that are clicked on while
 the app is open will be ignored. If you need to track them as well use manual reporting as described
 [here](https://appmetrica.yandex.ru/docs/mobile-sdk-dg/concepts/ios-operations.html#deeplink-tracking).
 */
@property (nonatomic, assign) BOOL appOpenTrackingEnabled;

/** Sets the ID of the user profile.

 @warning The value can contain up to 200 characters.
 */
@property (nonatomic, copy, nullable) NSString *userProfileID;

@end

NS_ASSUME_NONNULL_END
