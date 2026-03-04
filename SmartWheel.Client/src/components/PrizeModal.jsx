function PrizeModal({ prizeAmount, onClose }) {
  return (
    <div className="modal-overlay">
      <div className="modal-card prize-card">
        <h2 className="prize-title">🎉 Congratulations!</h2>

        <p className="prize-message">You won</p>

        <div className="prize-amount">R{prizeAmount}</div>

        <button className="modal-primary-button" onClick={onClose}>
          Awesome!
        </button>
      </div>
    </div>
  );
}

export default PrizeModal;
