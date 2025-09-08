using System;

namespace TallerPOO
{
    public class Time
    {
        private readonly int horas;
        private readonly int minutos;
        private readonly int segundos;
        private readonly int milisegundos;

        // === CONSTRUCTORES (sobrecarga) ===
        public Time() : this(0, 0, 0, 0) { }
        public Time(int horas) : this(horas, 0, 0, 0) { }
        public Time(int horas, int minutos) : this(horas, minutos, 0, 0) { }
        public Time(int horas, int minutos, int segundos) : this(horas, minutos, segundos, 0) { }
        public Time(int horas, int minutos, int segundos, int milisegundos)
        {
            ValidateOrThrow(horas, minutos, segundos, milisegundos);
            this.horas = horas;
            this.minutos = minutos;
            this.segundos = segundos;
            this.milisegundos = milisegundos;
        }

        // === Validación privada ===
        private static void ValidateOrThrow(int h, int m, int s, int ms)
        {
            if (h < 0 || h > 23) throw new Exception($"The hour: " + h + ", is not valid.");
            if (m < 0 || m > 59) throw new Exception($"The minute: " + m + ", is not valid.");
            if (s < 0 || s > 59) throw new Exception($"The second: " + s + ", is not valid.");
            if (ms < 0 || ms > 999) throw new Exception($"The millisecond: " + ms + ", is not valid.");
        }

        // === ToString (formato NO militar) ===
        public override string ToString()
        {
            int hour12 = horas % 12;
            if (hour12 == 0) hour12 = 12;
            string ampm = horas < 12 ? "AM" : "PM";
            return $"{hour12:00}:{minutos:00}:{segundos:00}.{milisegundos:000} {ampm}";
        }

        // === Conversiones ===
        public long ToMilliseconds()
        {
            return ((long)horas * 3600 + (long)minutos * 60 + segundos) * 1000 + milisegundos;
        }

        public int ToSeconds()
        {
            return horas * 3600 + minutos * 60 + segundos;
        }

        public int ToMinutes()
        {
            return horas * 60 + minutos;
        }

        // === Operaciones ===
        public bool IsOtherDay(Time other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            long sumMs = this.ToMilliseconds() + other.ToMilliseconds();
            return sumMs >= 24L * 60 * 60 * 1000; // 24 horas en ms
        }

        public Time Add(Time other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            int ms = this.milisegundos + other.milisegundos;
            int carryS = ms / 1000;
            ms %= 1000;

            int s = this.segundos + other.segundos + carryS;
            int carryM = s / 60;
            s %= 60;

            int m = this.minutos + other.minutos + carryM;
            int carryH = m / 60;
            m %= 60;

            int h = this.horas + other.horas + carryH;
            h %= 24;

            return new Time(h, m, s, ms);
        }
    }
}
