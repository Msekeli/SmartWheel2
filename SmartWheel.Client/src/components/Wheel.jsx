import { Wheel } from "react-custom-roulette";
import "../styles/wheel.css";

function RouletteWheel({ onSpinClick }) {
  const data = [
    { option: "No Prize" },
    { option: "R5" },
    { option: "R10" },
    { option: "R25" },
    { option: "R50" },
    { option: "R100" },
  ];

  return (
    <div className="wheel-section">
      <div className="wheel-wrapper">
        <div className="wheel-glow"></div>
        <div className="custom-pointer"></div>

        <Wheel
          mustStartSpinning={false}
          prizeNumber={0}
          data={data}
          startingOptionIndex={data.length - 1}
          spinDuration={0.6}
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
      </div>

      <button onClick={onSpinClick} className="spin-button">
        Spin the Wheel
      </button>
    </div>
  );
}

export default RouletteWheel;
