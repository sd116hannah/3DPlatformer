/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID JUMPPAD_BOUNCE = 155015579U;
        static const AkUniqueID PLAY_AMBIENCE = 278617630U;
        static const AkUniqueID PLAY_SPATIALAUDIO = 1887133776U;
        static const AkUniqueID PLAY_SYNTH_LAYER_1 = 2863702042U;
        static const AkUniqueID PLAYER_LAND = 3629196698U;
        static const AkUniqueID STOP_AMBIENCE = 2477713992U;
        static const AkUniqueID THUNDERSHOTA_BEGIN = 1553148048U;
        static const AkUniqueID THUNDERSHOTA_END = 2119183764U;
        static const AkUniqueID THUNDERSHOTA_INTERRUPT = 1763899454U;
        static const AkUniqueID THUNDERSHOTB_INTERRUPT = 4175013913U;
        static const AkUniqueID THUNDERSHOTB_PLAYSEQUENCE = 2469054401U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PAUSED = 319258907U;
            } // namespace STATE
        } // namespace GAMESTATE

        namespace LOCATION
        {
            static const AkUniqueID GROUP = 1176052424U;

            namespace STATE
            {
                static const AkUniqueID CAVE = 4122393694U;
                static const AkUniqueID JUNGLE = 219304270U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID VOID = 3370470011U;
            } // namespace STATE
        } // namespace LOCATION

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID JUMPPADCOUNTER = 3254916944U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MASTER = 4056684167U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCE = 85412153U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID MUSIC_GAMEPLAY = 620878633U;
        static const AkUniqueID MUSIC_MENU = 1598298728U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID SFX_ENEMIES = 2624074123U;
        static const AkUniqueID SFX_PLAYER = 217780010U;
        static const AkUniqueID UI = 1551306167U;
        static const AkUniqueID VO = 1534528548U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID REVERB_ROOM = 3258416929U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
