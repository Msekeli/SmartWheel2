import { useState } from "react";
import RouletteWheel from "./Wheel";
import EmailModal from "./EmailModal";
import RiddleModal from "./RiddleModal";
import { resolveIdentity, spinWheel } from "../services/smartWheelApi";

function SmartWheelLogic() {
  const [userId, setUserId] = useState(null);
  const [, setBalance] = useState(0);
  const [canSpin, setCanSpin] = useState(false);

  const [showEmailModal, setShowEmailModal] = useState(false);
  const [showRiddleModal, setShowRiddleModal] = useState(false);

  const [spinResult, setSpinResult] = useState(null);
  const [shouldSpin, setShouldSpin] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const handleSpinClick = () => {
    if (!userId) {
      setShowEmailModal(true);
      return;
    }

    if (!canSpin) {
      alert("You already spun today.");
      return;
    }

    // If we already have backend result → start animation
    if (spinResult) {
      setShouldSpin(true);
      return;
    }

    // Otherwise open riddles
    setShowRiddleModal(true);
  };

  const handleEmailSubmit = async (email) => {
    try {
      setIsLoading(true);

      const result = await resolveIdentity(email);

      setUserId(result.userId);
      setBalance(result.balance);
      setCanSpin(result.canSpin);

      setShowEmailModal(false);
    } catch (error) {
      console.error("Identity error:", error);
      alert("Failed to resolve user.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleRiddleSubmit = async (answers) => {
    try {
      setIsLoading(true);

      const result = await spinWheel(userId, answers);

      setSpinResult(result);
      setShowRiddleModal(false);
      // setShouldSpin(true);
    } catch (error) {
      console.error("Spin error:", error);
      alert("Spin failed.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleSpinComplete = () => {
    setShouldSpin(false);
    setSpinResult(null); // prevent re-spin without new riddles
  };

  return (
    <>
      <RouletteWheel
        onSpinClick={handleSpinClick}
        shouldSpin={shouldSpin}
        spinResult={spinResult}
        onSpinComplete={handleSpinComplete}
      />

      {showEmailModal && (
        <EmailModal
          onSubmit={handleEmailSubmit}
          onClose={() => setShowEmailModal(false)}
          isLoading={isLoading}
        />
      )}

      {showRiddleModal && (
        <RiddleModal
          onSubmit={handleRiddleSubmit}
          onClose={() => setShowRiddleModal(false)}
          isLoading={isLoading}
        />
      )}
    </>
  );
}

export default SmartWheelLogic;
