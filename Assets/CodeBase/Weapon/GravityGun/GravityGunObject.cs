using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Items.WeaponItems;
using CodeBase.StaticData.Items.WeaponItems.GravityGun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Weapon.GravityGun
{
    public class GravityGunObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider[] _allColliders;
        public Action OnStartGravity;
        public Action OnEndGravity;

        private SpringJoint _springJoint;
        private GravityGunConfig _config;
        private GravityObjectRememberSettings _rememberSettings;
        private List<Collider> _ignoreColliders;
        private Camera _mainCamera;

        private void Start()
        {
            _rememberSettings = new GravityObjectRememberSettings(_rigidbody.angularDrag, _rigidbody.drag);
            GetConfig();
            _ignoreColliders = AllServices.Container.Single<IGameFactory>().PlayerColliders;
            _mainCamera = Camera.main;
        }

        private void GetConfig()
        {
            var staticDataService = AllServices.Container.Single<IStaticDataService>();
            var config = staticDataService.ForWeapon(WeaponId.GravityGun_Gun);
            if (config is GravityGunConfig gravityConfig)
                _config = gravityConfig;
            else
                Debug.Log("failed to get settings");
        }

        public void StartGravityGun(Rigidbody connectRigidbody, Transform parent)
        {
            SetIgnoreColliders(true);
            OnStartGravity?.Invoke();
            _rigidbody.useGravity = false;
            SetSpring(connectRigidbody, parent);
            StartCoroutine(CheckBrakeConnectBody());
        }

        public void StopGravityGun()
        {
            transform.parent = null;

            if (_springJoint)
                Destroy(_springJoint);

            _rigidbody.useGravity = true;
            _rigidbody.angularDrag = _rememberSettings.RigidbodyAngularDrag;
            _rigidbody.drag = _rememberSettings.RigidbodyDrag;

            SetIgnoreColliders(false);
            AttackGravity();
            OnEndGravity?.Invoke();
            StopAllCoroutines();
        }

        private void AttackGravity()
        {
            _rigidbody.velocity = _mainCamera.transform.forward * _config.PushForce;
        }

        private void SetSpring(Rigidbody connectRigidbody, Transform parent)
        {
            transform.SetParent(parent);

            _springJoint = gameObject.AddComponent<SpringJoint>();
            _springJoint.connectedBody = connectRigidbody;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.anchor = Vector3.zero;

            _rigidbody.angularDrag = _config.RigidbodyAngularDrag;
            _rigidbody.drag = _config.RigidbodyDrag;
            _springJoint.connectedAnchor = Vector3.forward * _config.SpringConnectedAnchorOffsetZ;
            _springJoint.spring = _config.SpringSpring;
            _springJoint.damper = _config.SpringDamper;
            StartCoroutine(SetBrakeForce());
        }

        private void SetIgnoreColliders(bool isIgnore)
        {
            foreach (var gravityCollider in _allColliders)
                foreach (var ignoreCollider in _ignoreColliders)
                    Physics.IgnoreCollision(gravityCollider, ignoreCollider, isIgnore);
        }

        private IEnumerator SetBrakeForce()
        {
            yield return new WaitForSeconds(_config.TimeToSetBrakeForce);
            _springJoint.breakForce = _config.SpringBrakeForce;
        }

        private IEnumerator CheckBrakeConnectBody()
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            while (_springJoint != null && _springJoint.connectedBody != null)
            {
                yield return waitForEndOfFrame;
            }

            StopGravityGun();
        }
    }
}