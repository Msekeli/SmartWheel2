import { Wheel } from "react-custom-roulette";
import { useState } from "react";
import { spinWheel } from "../services/smartWheelApi";
import "../styles/wheel.css";

function RouletteWheel() {
  const [mustSpin, setMustSpin] = useState(false);
  const [prizeIndex, setPrizeIndex] = useState(0);
  const [winningAmount, setWinningAmount] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const data = [
    { option: "No Prize" },
    { option: "R5" },
    { option: "R10" },
    { option: "R25" },
    { option: "R50" },
    { option: "R100" },
  ];

  const handleSpinClick = async () => {
    if (mustSpin || isLoading) return;

    try {
      setIsLoading(true);

      // TEMP user + answers (replace later with real input)
      const userId = "00000000-0000-0000-0000-000000000001";
      const answers = ["echo", "shadow", "time", "fire", "water"];

      const result = await spinWheel(userId, answers);

      // Backend controls everything
      setPrizeIndex(result.wheelIndex);
      setWinningAmount(result.prizeAmount);

      setMustSpin(true);
    } catch (error) {
      console.error("Spin error:", error);
      alert("Something went wrong while spinning.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleStop = () => {
    setMustSpin(false);

    // Small dramatic pause
    setTimeout(() => {
      setShowModal(true);
    }, 600);
  };

  return (
    <div className="wheel-section">
      <div className="wheel-wrapper">
        <div className="wheel-glow"></div>
        <div className="custom-pointer"></div>

        <Wheel
          mustStartSpinning={mustSpin}
          prizeNumber={prizeIndex}
          data={data}
          startingOptionIndex={data.length - 1}
          spinDuration={0.6}
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
        disabled={mustSpin || isLoading}
        className="spin-button"
      >
        {isLoading ? "Processing..." : "Spin the Wheel"}
      </button>

      {showModal && (
        <div className="modal-overlay">
          <div className="modal-card">
            <div className="modal-title">🎉 Congratulations!</div>
            <div>
              You’ve won <strong>R{winningAmount}</strong>
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
