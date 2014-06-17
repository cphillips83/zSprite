using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zSprite.EntitySystem.Systems;

namespace zSprite
{
    /**
     * This system processes input, sending it out as events against the LocalPlayer entity.
     * <p/>
     * In addition to raw keyboard and mouse input, the system handles Bind Buttons and Bind Axis, which can be mapped
     * to one or more inputs.
     */
    public class InputSystem : BaseComponentSystem
    {

        //@In
        private Config config;

        //@In
        private GameEngine engine;

        private MouseDevice mouse = new NullMouseDevice();
        private KeyboardDevice keyboard = new NullKeyboardDevice();

        private Dictionary<string, BindableAxisImpl> axisLookup = new Dictionary<string, BindableAxisImpl>();
        private Dictionary<SimpleUri, BindableButtonImpl> buttonLookup = new Dictionary<SimpleUri, BindableButtonImpl>();

        private List<BindableAxisImpl> axisBinds = Lists.newArrayList();
        private List<BindableButtonImpl> buttonBinds = Lists.newArrayList();

        // Links between primitive inputs and bind buttons
        private Dictionary<int, BindableButtonImpl> keyBinds = new Dictionary<int, BindableButtonImpl>();
        private Dictionary<MouseInput, BindableButtonImpl> mouseButtonBinds = new Dictionary<MouseInput, BindableButtonImpl>();
        private BindableButtonImpl mouseWheelUpBind;
        private BindableButtonImpl mouseWheelDownBind;

        private LocalPlayer localPlayer;
        private CameraTargetSystem targetSystem;

        public override void initialise()
        {
            localPlayer = CoreRegistry.get<LocalPlayer>();
            targetSystem = CoreRegistry.get<CameraTargetSystem>();
        }

        public override void shutdown()
        {
            localPlayer = null;
            targetSystem = null;
        }

        public void setMouseDevice(MouseDevice mouseDevice)
        {
            this.mouse = mouseDevice;
        }

        public void setKeyboardDevice(KeyboardDevice keyboardDevice)
        {
            this.keyboard = keyboardDevice;
        }

        public MouseDevice getMouseDevice()
        {
            return mouse;
        }

        public KeyboardDevice getKeyboard()
        {
            return keyboard;
        }

        public BindableButton registerBindButton(SimpleUri bindId, String displayName)
        {
            return registerBindButton(bindId, displayName, new BindButtonEvent());
        }

        public BindableButton registerBindButton(SimpleUri bindId, String displayName, BindButtonEvent @event)
        {
            BindableButtonImpl bind = new BindableButtonImpl(bindId, displayName, @event);
            buttonLookup.put(bindId, bind);
            buttonBinds.add(bind);
            return bind;
        }

        public void clearBinds()
        {
            buttonLookup.Clear();
            buttonBinds.Clear();
            axisLookup.Clear();
            axisBinds.Clear();
            keyBinds.Clear();
            mouseButtonBinds.Clear();
            mouseWheelUpBind = null;
            mouseWheelDownBind = null;
        }

        public BindableButton getBindButton(SimpleUri bindId)
        {
            BindableButton button;
            if (buttonLookup.TryGetValue(bindId, out button))
                return button;

            return null;
        }

        public void linkBindButtonToInput(Input input, SimpleUri bindId)
        {
            switch (input.getType())
            {
                case KEY:
                    linkBindButtonToKey(input.getId(), bindId);
                    break;
                case MOUSE_BUTTON:
                    MouseInput button = MouseInput.find(input.getType(), input.getId());
                    linkBindButtonToMouse(button, bindId);
                    break;
                case MOUSE_WHEEL:
                    linkBindButtonToMouseWheel(input.getId(), bindId);
                    break;
                default:
                    break;
            }
        }

        public void linkBindButtonToInput(InputEvent input, SimpleUri bindId)
        {
            if (input is KeyEvent)
            {
                linkBindButtonToKey(((KeyEvent)input).getKey().getId(), bindId);
            }
            else if (input is MouseButtonEvent)
            {
                linkBindButtonToMouse(((MouseButtonEvent)input).getButton(), bindId);
            }
            else if (input is MouseWheelEvent)
            {
                linkBindButtonToMouseWheel(((MouseWheelEvent)input).getWheelTurns(), bindId);
            }
        }

        public void linkBindButtonToKey(int key, SimpleUri bindId)
        {
            BindableButtonImpl bindInfo = buttonLookup.get(bindId);
            keyBinds.put(key, bindInfo);
        }

        public void linkBindButtonToMouse(MouseInput mouseButton, SimpleUri bindId)
        {
            BindableButtonImpl bindInfo = buttonLookup.get(bindId);
            mouseButtonBinds.put(mouseButton, bindInfo);
        }

        public void linkBindButtonToMouseWheel(int direction, SimpleUri bindId)
        {
            if (direction > 0)
            {
                mouseWheelDownBind = buttonLookup.get(bindId);
            }
            else if (direction < 0)
            {
                mouseWheelUpBind = buttonLookup.get(bindId);
            }
        }

        public BindableAxis registerBindAxis(String id, BindableButton positiveButton, BindableButton negativeButton)
        {
            return registerBindAxis(id, new BindAxisEvent(), positiveButton, negativeButton);
        }

        public BindableAxis registerBindAxis(String id, BindAxisEvent @event, SimpleUri positiveButtonId, SimpleUri negativeButtonId)
        {
            return registerBindAxis(id, @event, getBindButton(positiveButtonId), getBindButton(negativeButtonId));
        }

        public BindableAxis registerBindAxis(String id, BindAxisEvent @event, BindableButton positiveButton, BindableButton negativeButton)
        {
            BindableAxisImpl axis = new BindableAxisImpl(id, @event, positiveButton, negativeButton);
            axisBinds.add(axis);
            axisLookup.put(id, axis);
            return axis;
        }

        public void update(float delta)
        {
            processMouseInput(delta);
            processKeyboardInput(delta);
            processBindRepeats(delta);
            processBindAxis(delta);
        }

        private void processMouseInput(float delta) {
        if (!engine.hasFocus()) {
            return;
        }

        Vector2i deltaMouse = mouse.getDelta();
        //process mouse movement x axis
        if (deltaMouse.x != 0) {
            MouseAxisEvent @event = new MouseXAxisEvent(deltaMouse.x * config.getInput().getMouseSensitivity(), delta);
            setupTarget(@event);
            for (EntityRef entity : getInputEntities()) {
                entity.send(@event);
                if (@event.isConsumed()) {
                    break;
                }
            }
        }

        //process mouse movement y axis
        if (deltaMouse.y != 0) {
            MouseAxisEvent @event = new MouseYAxisEvent(deltaMouse.y * config.getInput().getMouseSensitivity(), delta);
            setupTarget(@event);
            for (EntityRef entity : getInputEntities()) {
                entity.send(@event);
                if (@event.isConsumed()) {
                    break;
                }
            }
        }

        //process mouse clicks
        for (InputAction action : mouse.getInputQueue()) {
            switch (action.getInput().getType()) {
                case MOUSE_BUTTON:
                    int id = action.getInput().getId();
                    if (id != -1) {
                        MouseInput button = MouseInput.find(action.getInput().getType(), action.getInput().getId());
                        bool consumed = sendMouseEvent(button, action.getState().isDown(), action.getMousePosition(), delta);

                        BindableButtonImpl bind = mouseButtonBinds.get(button);
                        if (bind != null) {
                            bind.updateBindState(
                                    action.getInput(),
                                    action.getState().isDown(),
                                    delta,
                                    getInputEntities(),
                                    targetSystem.getTarget(),
                                    targetSystem.getTargetBlockPosition(),
                                    targetSystem.getHitPosition(),
                                    targetSystem.getHitNormal(),
                                    consumed
                            );
                        }
                    }
                    break;
                case MOUSE_WHEEL:
                    int dir = action.getInput().getId();
                    if (dir != 0 && action.getTurns() != 0) {
                        bool consumed = sendMouseWheelEvent(action.getMousePosition(), dir * action.getTurns(), delta);

                        BindableButtonImpl bind = (dir == 1) ? mouseWheelUpBind : mouseWheelDownBind;
                        if (bind != null) {
                            for (int i = 0; i < action.getTurns(); ++i) {
                                bind.updateBindState(
                                        action.getInput(),
                                        true,
                                        delta,
                                        getInputEntities(),
                                        targetSystem.getTarget(),
                                        targetSystem.getTargetBlockPosition(),
                                        targetSystem.getHitPosition(),
                                        targetSystem.getHitNormal(),
                                        consumed
                                );
                                bind.updateBindState(
                                        action.getInput(),
                                        false,
                                        delta,
                                        getInputEntities(),
                                        targetSystem.getTarget(),
                                        targetSystem.getTargetBlockPosition(),
                                        targetSystem.getHitPosition(),
                                        targetSystem.getHitNormal(),
                                        consumed
                                );
                            }
                        }
                    }
                    break;
                case KEY:
                    break;
            }
        }
    }

        private void setupTarget(InputEvent @event)
        {
            if (targetSystem.isTargetAvailable())
            {
                @event.setTargetInfo(targetSystem.getTarget(), targetSystem.getTargetBlockPosition(), targetSystem.getHitPosition(), targetSystem.getHitNormal());
            }
        }

        private void processKeyboardInput(float delta) {
        for (InputAction action : keyboard.getInputQueue()) {
            bool consumed = sendKeyEvent(action.getInput(), action.getInputChar(), action.getState(), delta);

            // Update bind
            BindableButtonImpl bind = keyBinds.get(action.getInput().getId());
            if (bind != null && action.getState() != ButtonState.REPEAT) {
                bind.updateBindState(
                        action.getInput(),
                        (action.getState() == ButtonState.DOWN),
                        delta, getInputEntities(),
                        targetSystem.getTarget(),
                        targetSystem.getTargetBlockPosition(),
                        targetSystem.getHitPosition(),
                        targetSystem.getHitNormal(),
                        consumed
                );
            }
        }
    }

        private void processBindAxis(float delta) {
        for (BindableAxisImpl axis : axisBinds) {
            axis.update(getInputEntities(), delta, targetSystem.getTarget(), targetSystem.getTargetBlockPosition(),

                    targetSystem.getHitPosition(), targetSystem.getHitNormal());
        }
    }

        private void processBindRepeats(float delta) {
        for (BindableButtonImpl button : buttonBinds) {
            button.update(getInputEntities(), delta, targetSystem.getTarget(), targetSystem.getTargetBlockPosition(),
                    targetSystem.getHitPosition(), targetSystem.getHitNormal());
        }
    }

        private bool sendKeyEvent(Input key, char keyChar, ButtonState state, float delta) {
        KeyEvent @event;
        switch (state) {
            case UP:
                @event = KeyUpEvent.create(key, keyChar, delta);
                break;
            case DOWN:
                @event = KeyDownEvent.create(key, keyChar, delta);
                break;
            case REPEAT:
                @event = KeyRepeatEvent.create(key, keyChar, delta);
                break;
            default:
                return false;
        }
        setupTarget(@event);
        for (EntityRef entity : getInputEntities()) {
            entity.send(@event);
            if (@event.isConsumed()) {
                break;
            }
        }

        bool consumed = @event.isConsumed();
        @event.reset();
        return consumed;
    }

        private bool sendMouseEvent(MouseInput button, bool buttonDown, Vector2i position, float delta) {
        MouseButtonEvent @event;
        switch (button) {
            case NONE:
                return false;
            case MOUSE_LEFT:
                @event = (buttonDown) ? LeftMouseDownButtonEvent.create(position, delta) : LeftMouseUpButtonEvent.create(position, delta);
                break;
            case MOUSE_RIGHT:
                @event = (buttonDown) ? RightMouseDownButtonEvent.create(position, delta) : RightMouseUpButtonEvent.create(position, delta);
                break;
            default:
                @event = (buttonDown) ? MouseDownButtonEvent.create(button, position, delta) : MouseUpButtonEvent.create(button, position, delta);
                break;
        }
        setupTarget(@event);
        for (EntityRef entity : getInputEntities()) {
            entity.send(@event);
            if (@event.isConsumed()) {
                break;
            }
        }
        bool consumed = @event.isConsumed();
        @event.reset();
        return consumed;
    }

        private bool sendMouseWheelEvent(Vector2i pos, int wheelTurns, float delta) {
            MouseWheelEvent mouseWheelEvent = new MouseWheelEvent(pos, wheelTurns, delta);
            setupTarget(mouseWheelEvent);
            for (EntityRef entity : getInputEntities()) {
                entity.send(mouseWheelEvent);
                if (mouseWheelEvent.isConsumed()) {
                    break;
                }
            }
            return mouseWheelEvent.isConsumed();
        }

        private EntityRef[] getInputEntities()
        {
            return new EntityRef[] { localPlayer.getClientEntity(), localPlayer.getCharacterEntity() };
        }

    }
}
