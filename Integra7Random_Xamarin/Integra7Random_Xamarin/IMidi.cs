﻿using System;
using Xamarin.Forms;

namespace Integra7Random_Xamarin
{
    public interface IMidi
    {
        void Init(Integra7Random_Xamarin.MainPage mainPage, String deviceName, Picker OutputDeviceSelector, Picker InputDeviceSelector, object DeviceSpecificObject, byte MidiOutPortChannel, byte MidiInPortChannel);

        Boolean MidiIsReady();

        void NoteOn(byte currentChannel, byte noteNumber, byte velocity);

        void NoteOff(byte currentChannel, byte noteNumber);

        void SendControlChange(byte channel, byte controller, byte value);

        void SetVolume(byte currentChannel, byte volume);

        void ProgramChange(byte currentChannel, String smsb, String slsb, String spc);

        void ProgramChange(byte currentChannel, byte msb, byte lsb, byte pc);

        void SendSystemExclusive(byte[] bytes);

        byte[] SystemExclusiveDT1Message(byte[] Address, byte[] DataToTransmit);

        byte[] SystemExclusiveRQ1Message(byte[] Address, byte[] Length);

        void OutputDeviceChanged(Picker DeviceSelector);

        void InputDeviceChanged(Picker DeviceSelector);

        byte GetMidiOutPortChannel();

        void SetMidiOutPortChannel(byte OutPortChannel);

        byte GetMidiInPortChannel();

        void SetMidiInPortChannel(byte InPortChannel);

        void AllNotesOff(byte currentChannel);
    }
}
