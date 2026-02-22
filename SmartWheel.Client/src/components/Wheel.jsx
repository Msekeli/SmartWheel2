import { Wheel } from "react-custom-roulette";
import { useState } from "react";
import "../styles/wheel.css";

function RouletteWheel() {
  const [mustSpin, setMustSpin] = useState(false);
  const [prizeIndex, setPrizeIndex] = useState(0);
  const [showModal, setShowModal] = useState(false);

  const data = [
    { option: "No Prize" },
    { option: "R5" },
    { option: "R10" },
    { option: "R25" },
    { option: "R50" },
    { option: "R100" },
  ];

  const handleSpinClick = () => {
    const randomIndex = Math.floor(Math.random() * data.length);
    setPrizeIndex(randomIndex);
    setMustSpin(true);
  };

  const handleStop = () => {
    setMustSpin(false);
    setShowModal(true);
  };

  return (
    <div className="wheel-section">
      <div className="wheel-wrapper">
        <div className={`wheel-glow ${mustSpin ? "spinning" : ""}`}></div>
        <div className="custom-pointer"></div>

        <Wheel
          mustStartSpinning={mustSpin}
          prizeNumber={prizeIndex}
          data={data}
          onStopSpinning={handleStop}
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

      <button
        onClick={handleSpinClick}
        disabled={mustSpin}
        className="spin-button"
      >
        Spin the Wheel
      </button>

      {showModal && (
        <div className="modal-overlay">
          <div className="modal-card">
            <div className="modal-title">🎉 Congratulations!</div>
            <div>
              You’ve won <strong>{data[prizeIndex].option}</strong>
            </div>
            <button
              className="modal-button"
              onClick={() => setShowModal(false)}
            >
              Close
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

export default RouletteWheel;
