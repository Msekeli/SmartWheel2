import { useState } from "react";
import RouletteWheel from "./Wheel";
import EmailModal from "./EmailModal";
import RiddleModal from "./RiddleModal";
import PrizeModal from "./PrizeModal";
import MessageModal from "./MessageModal";

import {
  resolveIdentity,
  spinWheel,
  getStatus,
} from "../services/smartWheelApi";

function SmartWheelLogic() {
  const [userId, setUserId] = useState(null);
  const [_balance, setBalance] = useState(0);
  const [canSpin, setCanSpin] = useState(false);

  const [showEmailModal, setShowEmailModal] = useState(false);
  const [showRiddleModal, setShowRiddleModal] = useState(false);
  const [showPrizeModal, setShowPrizeModal] = useState(false);

  const [spinResult, setSpinResult] = useState(null);
  const [shouldSpin, setShouldSpin] = useState(false);
  const [prizeAmount, setPrizeAmount] = useState(null);

  const [message, setMessage] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

  const resetSpinState = () => {
    setSpinResult(null);
    setPrizeAmount(null);
    setShouldSpin(false);
  };

  const handleSpinClick = () => {
    if (!userId) {
      setShowEmailModal(true);
      return;
    }

    if (!canSpin) {
      setMessage("⏳ You already spun today. Please come back tomorrow.");
      return;
    }

    if (!spinResult) {
      setShowRiddleModal(true);
      return;
    }

    setShouldSpin(true);
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
      console.error(error);
      setMessage("⚠️ Failed to verify email. Please try again.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleRiddleSubmit = async (answers) => {
    try {
      setIsLoading(true);

      const result = await spinWheel(userId, answers);

      setSpinResult(result);
      setPrizeAmount(result.prizeAmount);

      setShowRiddleModal(false);

      setMessage("🧠 Great! Click Spin to reveal your prize.");
    } catch (error) {
      console.error(error);
      setMessage("⚠️ Something went wrong while spinning.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleSpinComplete = async () => {
    setShouldSpin(false);
    setShowPrizeModal(true);

    try {
      const status = await getStatus(userId);

      setBalance(status.balance);
      setCanSpin(status.canSpin);
    } catch (error) {
      console.error("Status refresh failed", error);
    }
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

      {showPrizeModal && (
        <PrizeModal
          prizeAmount={prizeAmount}
          onClose={() => {
            setShowPrizeModal(false);
            resetSpinState();
          }}
        />
      )}

      {message && (
        <MessageModal message={message} onClose={() => setMessage(null)} />
      )}
    </>
  );
}

export default SmartWheelLogic;
