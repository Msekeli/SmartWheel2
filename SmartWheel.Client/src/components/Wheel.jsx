import { Wheel } from "react-custom-roulette";
import { useEffect, useRef } from "react";
import "../styles/wheel.css";
import "../styles/pointer.css";
import "../styles/button.css";
import "../styles/quiz.css";
import "../styles/modal.css";

// import tickSound from "../assets/sounds/tick.mp3";

function RouletteWheel({
  onSpinClick,
  shouldSpin,
  spinResult,
  onSpinComplete,
}) {
  const data = [
    { option: "No Prize" },
    { option: "R5" },
    { option: "R10" },
    { option: "R25" },
    { option: "R50" },
    { option: "R100" },
  ];

  const safeIndex =
    spinResult && spinResult.wheelIndex < data.length
      ? spinResult.wheelIndex
      : 0;

  const tickAudio = useRef(null);
  const tickInterval = useRef(null);

  useEffect(() => {
    if (shouldSpin) {
      tickInterval.current = setInterval(() => {
        if (tickAudio.current) {
          tickAudio.current.currentTime = 0;
          tickAudio.current.play().catch(() => {});
        }
      }, 120);
    } else {
      clearInterval(tickInterval.current);
    }

    return () => clearInterval(tickInterval.current);
  }, [shouldSpin]);

  return (
    <div className="wheel-section">
      <div className="wheel-wrapper">
        <div className={`wheel-glow ${shouldSpin ? "spinning" : ""}`}></div>

        {/* Custom pointer */}
        <div className="custom-pointer"></div>

        <Wheel
          mustStartSpinning={shouldSpin}
          prizeNumber={safeIndex}
          onStopSpinning={onSpinComplete}
          data={data}
          spinDuration={2}
          backgroundColors={[
            "#7c3aed",
            "#38bdf8",
            "#facc15",
            "#4c1d95",
            "#0ea5e9",
            "#eab308",
          ]}
          textColors={["#ffffff"]}
          outerBorderColor="#d4af37"
          outerBorderWidth={16}
          innerBorderColor="#ffffff"
          innerBorderWidth={3}
          radiusLineColor="#ffffff"
          radiusLineWidth={3}
          fontSize={14}
          pointerProps={{ style: { display: "none" } }}
        />

        {/* audio element */}
        {/* <audio ref={tickAudio} src={tickSound} preload="auto" /> */}
      </div>

      <button
        onClick={onSpinClick}
        className="spin-button"
        disabled={shouldSpin}
      >
        Spin the Wheel
      </button>
    </div>
  );
}

export default RouletteWheel;
